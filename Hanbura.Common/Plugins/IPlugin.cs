using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Plugins
{
	/// <summary>
	/// プラグイン
	/// </summary>
	public interface IPlugin
	{
		/// <summary>
		/// プラグインIDを取得する
		/// </summary>
		 Guid PluginId { get; }

		/// <summary>
		/// プラグイン名を取得する
		/// </summary>
		 string PluginName { get; }

		/// <summary>
		/// プラグインの製作者を表す文字列を取得する
		/// </summary>
		 string Author { get; }

		/// <summary>
		/// プラグインの著作権情報を表す文字列を取得する
		/// </summary>
		 string CopyrightString { get; }

		/// <summary>
		/// 作者のWebサイトなどのURIを取得する
		/// </summary>
		string Uri { get; }

		/// <summary>
		/// プラグインのバージョン情報を取得する
		/// </summary>
		 Version Version { get; }

		/// <summary>
		/// プラグインがロードされ、インスタンスが作成された時に呼び出される
		/// </summary>
		void OnLoaded();

		/// <summary>
		/// プラグインがアンロードされる時に呼び出される
		/// </summary>
		void OnUnloading();

		/// <summary>
		/// プラグインの初期化を行う。
		/// プラグインがロードされ、アプリケーションが初期化された後に呼び出される。
		/// ダイアログを表示しても良い。
		/// </summary>
		/// <param name="pluginInfo">プラグイン情報</param>
		void Initialize(IPluginInfo pluginInfo);
	}
}
