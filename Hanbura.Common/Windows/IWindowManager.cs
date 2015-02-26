using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Windows
{
	interface IWindowManager
	{
		/// <summary>
		/// ウィンドウを生成する。
		/// </summary>
		/// <param name="tag">子ウィンドウを識別するタグを半角英数で指定します。</param>
		/// <param name="info"></param>
		/// <param name="owner"></param>
		/// <remarks>tagは子ウィンドウの識別や設定保存に利用されるため、重複を避けた名前にすること。</remarks>
		/// <exception cref="Exceptions">指定したタグのウィンドウが既に存在する。</exception>
		IWindow CreateWindow(
			string tag,
			WindowConfig config,
			IWindow owner = null);

		/// <summary>
		/// ウィンドウを探す
		/// </summary>
		/// <param name="tag">ウィンドウのタグ</param>
		/// <returns>タグに一致するウィンドウ</returns>
		IWindow FindWindow(string tag);

		/// <summary>
		/// 登録されているウィンドウを全て取得する。
		/// </summary>
		IEnumerable<IWindow> Windows { get; }

		/// <summary>
		/// 登録されているウィンドウのタグを全て取得する。
		/// </summary>
		IEnumerable<string> Tags { get; }

		/// <summary>
		/// UIスレッドのディスパッチャを取得する。
		/// </summary>
		IDispatcher UIThreadDispatcher { get; }
	}
}
