using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Plugins
{
	/// <summary>
	/// ボタンクリックで何か操作をするなどといったアクションを提供するプラグイン
	/// </summary>
	public interface IActionPlugin : INonGamePlugin
	{
		/// <summary>
		/// キャプションバーに表示する文字列を取得する。
		/// </summary>
		string CaptionButtonString { get; }

		/// <summary>
		/// アクションの名前を取得する。
		/// </summary>
		string ActionName { get; set; }

		/// <summary>
		/// アクションの説明を取得する。
		/// </summary>
		string Description { get; }

		/// <summary>
		/// アクションを実行する。
		/// </summary>
		void DoAction();
	}
}
