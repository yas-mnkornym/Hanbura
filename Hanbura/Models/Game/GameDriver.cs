using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Fiddler;
using Studiotaiha.Hanbura.Plugins;
using Studiotaiha.Hanbura.Plugins.Gaming;

namespace Studiotaiha.Hanbura.Models.Game
{
	internal class GameDriver : IDisposable
	{
		IGameHandler handler_;
		GameProxy proxy_;
		WebBrowser webBrowser_;

		public GameDriver(
			WebBrowser webBrowser,
			GameProxy proxy,
			IGameHandler handler)
		{
			if (webBrowser == null) { throw new ArgumentNullException("webBrowser"); }
			if (proxy == null) { throw new ArgumentNullException("proxy"); }
			if (handler == null) { throw new ArgumentNullException("handler"); }
			webBrowser_ = webBrowser;
			proxy_ = proxy;
			handler_ = handler;
		}

		public void Start()
		{
			proxy_.Start(handler_);
			handler_.Register(webBrowser_);

			var url = handler_.GameUrl;
			webBrowser_.Navigate(url);
		}

		public void Stop()
		{
			handler_.Unregister();
			proxy_.Stop();
		}

		#region IDisposable メンバ
		bool isDisposed_ = false;
		virtual protected void Dispose(bool disposing)
		{
			if (isDisposed_) { return; }
			if (disposing) {
				try {
					Stop();
				}
				catch (Exception ex) {
					Logger.Error("ゲームの停止に失敗しました。", ex);
				}

				if (proxy_ != null) {
					proxy_.Dispose();
					proxy_ = null;
				}
			}
			isDisposed_ = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

		#region Logger
		ILogger logger_;
		ILogger Logger
		{
			get
			{
				return logger_ ?? (logger_ = LoggingService.Current.GetLogger(this));
			}
		}
		#endregion
	}
}
