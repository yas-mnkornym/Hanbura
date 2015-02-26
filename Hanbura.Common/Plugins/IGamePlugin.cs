using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Studiotaiha.Hanbura.Plugins.Gaming;

namespace Studiotaiha.Hanbura.Plugins
{
	public interface IGamePlugin : IPlugin
	{
		/// <summary>
		/// ゲームを表すIDを取得する。
		/// </summary>
		string GameID { get; }

		/// <summary>
		/// ゲームハンドラを取得する
		/// </summary>
		IGameHandler GemeHandler { get; }

		/// <summary>
		/// ログイン処理を開始したことを示すイベント
		/// </summary>
		event EventHandler LoginStarted;

		/// <summary>
		/// ログイン処理が完了したことを示すイベント
		/// </summary>
		event EventHandler<LoginFinishedEventArgs> LoggedFinished;

		/// <summary>
		/// ゲームが開始された事を示すイベント
		/// </summary>
		event EventHandler GameStarted;

		/// <summary>
		/// エラーが発生したことを示すイベント
		/// </summary>
		event EventHandler<GameErrorEventArgs> Error;
	}

	public class LoginFinishedEventArgs : EventArgs
	{
		public LoginFinishedEventArgs(ELoginResult result)
		{
			Result = result;
		}

		public ELoginResult Result { get; private set; }
	}

	public enum ELoginResult
	{
		/// <summary>
		/// ログイン成功
		/// </summary>
		Succeeded = 0,

		/// <summary>
		/// IDかパスワードが不正
		/// </summary>
		InvalidIDOrPassword,

		/// <summary>
		/// IDかパスワードが空
		/// </summary>
		IDOrPasswordAreEmpty,
	}

	public class GameErrorEventArgs : EventArgs
	{
		/// <summary>
		/// エラーメッセージ
		/// </summary>
		public string Message { get; private set; }

		/// <summary>
		/// 例外情報を取得する
		/// </summary>
		public Exception Exception { get; private set; }

		/// <summary>
		/// エラーの種別
		/// </summary>
		public GameErrorKind Kind { get; private set; }
	}

	/// <summary>
	/// ゲームエラーの種別を表す列挙型
	/// </summary>
	public enum GameErrorKind
	{
		/// <summary>
		/// 不明
		/// </summary>
		Unknown = 0,
		
		/// <summary>
		/// ログインに失敗した
		/// </summary>
		LoginError = 1,

		/// <summary>
		/// ゲームデータのパースに失敗した
		/// </summary>
		ParseError = 2,
	}
}
