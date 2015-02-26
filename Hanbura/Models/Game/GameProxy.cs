using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler;
using Studiotaiha.Hanbura.Plugins.Gaming;

namespace Studiotaiha.Hanbura.Models.Game
{
	internal class GameProxy : IDisposable
	{
		int port_;
		IGameHandler handler_;
		IExtendedGameHandler exHandler_ = null;
		ProxySettings upperProxy_ = null;

		public GameProxy(
			int port,
			ProxySettings upperProxy
			)
		{
			if (port <= 0 || 0xffff < port) { throw new ArgumentOutOfRangeException("port"); }
			if (upperProxy == null) { throw new ArgumentNullException("upperProxy"); }
			port_ = port;
			upperProxy_ = upperProxy;
		}

		public void Start(IGameHandler handler)
		{
			if (handler == null) { throw new ArgumentNullException("handler"); }
			handler_ = handler;
			exHandler_ = handler as IExtendedGameHandler;

			FiddlerApplication.BeforeRequest += FiddlerApplication_BeforeRequest;
			FiddlerApplication.BeforeResponse += FiddlerApplication_BeforeResponse;
			FiddlerApplication.Startup(port_, FiddlerCoreStartupFlags.DecryptSSL | FiddlerCoreStartupFlags.ChainToUpstreamGateway);
			URLMonInterop.SetProxyInProcess(string.Format("localhost:{0}", FiddlerApplication.oProxy.ListenPort), "<local>");
		}

		public void Stop()
		{
			FiddlerApplication.BeforeRequest += FiddlerApplication_BeforeRequest;
			FiddlerApplication.BeforeResponse += FiddlerApplication_BeforeResponse;
			FiddlerApplication.Startup(port_, FiddlerCoreStartupFlags.DecryptSSL | FiddlerCoreStartupFlags.ChainToUpstreamGateway);
			URLMonInterop.ResetProxyInProcessToDefault();
			handler_ = null;
			exHandler_ = null;
		}

		void FiddlerApplication_BeforeRequest(Session oSession)
		{
			bool shouldAppendProxy = false;

			// 処理させる
			if (exHandler_ != null) {
				shouldAppendProxy = exHandler_.OnRequestingEx(oSession);
			}
			else {
				var url = oSession.fullUrl;
				var body = oSession.GetRequestBodyAsString();
				var method = oSession.RequestMethod;
				if(handler_.OnRequesting(url, body, method)){
					shouldAppendProxy = true;
				}
				else{
					oSession.Ignore();
				}
			}

			// 必要ならプロキシを設定する
			if (shouldAppendProxy && upperProxy_ != null) {
				oSession["X-OverrideGateway"] = upperProxy_.ToString();
			}
		}

		void FiddlerApplication_BeforeResponse(Session oSession)
		{
			if (exHandler_ != null) {
				exHandler_.OnResponsedEx(oSession);
			}
			else {
				var url = oSession.fullUrl;
				var requestBody = oSession.GetRequestBodyAsString();
				var responseBody = oSession.GetResponseBodyAsString();
				var method = oSession.RequestMethod;
				handler_.OnResponsed(url, requestBody, responseBody, method);
			}
		}

		#region IDisposable メンバ
		bool isDisposed_ = false;
		virtual protected void Dispose(bool disposing)
		{
			if (isDisposed_) { return; }
			if (disposing) {
				// Write your own disposing code here.
				Stop();
			}
			isDisposed_ = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
