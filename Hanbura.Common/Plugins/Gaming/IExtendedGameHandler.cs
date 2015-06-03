using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiddler;

namespace Studiotaiha.Hanbura.Plugins.Gaming
{
	/// <summary>
	/// FiddlerCoreのセッション情報を直接取り扱うゲームハンドラ
	/// </summary>
	/// <remarks>
	/// OnRequesting()およびOnresponsed()は呼び出されない。
	/// </remarks>
	public interface IExtendedGameHandler : IGameHandler
	{
		/// <summary>
		/// リクエストが送信される時に呼び出される。
		/// </summary>
		/// <param name="url">リクエストURL</param>
		/// <param name="body">リクエストボディを表す文字列</param>
		/// <param name="method">メソッド</param>
		/// <returns>oSession.Ignore()を呼び出した場合は必ずfalseを返す。それ以外はtrueを返す。</returns>
		/// <remarks>この関数は通信スレッド上で呼び出されるため、負荷がかかる場合はプラグイン側で別スレッドに処理を逃がすこと。</remarks>
		bool OnRequestingEx(Session oSession);

		/// <summary>
		/// レスポンスを受信した時に呼び出される。
		/// </summary>
		/// <param name="url">リクエストURL</param>
		/// <param name="response">レスポンスデータを表す文字列</param>
		/// <param name="method">リクエストメソッド</param>
		/// <remarks>この関数は通信スレッド上で呼び出されるため、負荷がかかる場合はプラグイン側で別スレッドに処理を逃がすこと。</remarks>
		void OnResponsedEx(Session oSession);
	}
}
