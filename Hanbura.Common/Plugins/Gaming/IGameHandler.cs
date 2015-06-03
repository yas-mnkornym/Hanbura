using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Studiotaiha.Hanbura.Plugins.Gaming
{
	public interface IGameHandler
	{
		/// <summary>
		/// ゲームのURLを取得する
		/// </summary>
		string GameUrl { get; }

		/// <summary>
		/// ゲームのURLが取得され、ナビゲートする前に呼び出される
		/// </summary>
		/// <param name="webBrowser">ゲームをホストするWebBrowserコントロール</param>
		void Register(WebBrowser webBrowser);

		/// <summary>
		/// ゲームが終了される時に呼び出される。
		/// プラグインはゲームに関わるリソースを開放しなければならない（WebBrowserのイベントハンドラも含む）。
		/// </summary>
		void Unregister();

		/// <summary>
		/// リクエストが送信される時に呼び出される。
		/// </summary>
		/// <param name="url">リクエストURL</param>
		/// <param name="body">リクエストボディを表す文字列</param>
		/// <param name="method">メソッド</param>
		/// <returns>falseを返すと、このセッションを無視する。</returns>
		/// <remarks>この関数は通信スレッド上で呼び出されるため、負荷がかかる場合はプラグイン側で別スレッドに処理を逃がすこと。</remarks>
		bool OnRequesting(
			string url,
			string body,
			string method);

		/// <summary>
		/// レスポンスを受信した時に呼び出される。
		/// </summary>
		/// <param name="url">リクエストURL</param>
		/// <param name="requestbody">リクエストデータを表す文字列</param>
		/// <param name="responseBody">レスポンスデータを表す文字列</param>
		/// <param name="method">リクエストメソッド</param>
		/// <remarks>この関数は通信スレッド上で呼び出されるため、負荷がかかる場合はプラグイン側で別スレッドに処理を逃がすこと。</remarks>
		void OnResponsed(
			string url,
			string requestbody,
			string responseBody,
			string method);
	}
}
