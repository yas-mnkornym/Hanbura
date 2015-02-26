using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura
{
	internal static class Constants
	{
		/// <summary>
		/// ログファイル名
		/// </summary>
		public static readonly string LogFileName = "Hanbura.log";

		/// <summary>
		/// 設定ファイル名
		/// </summary>
		public static readonly string SettingsFileName = "Hanbura.xml";

		/// <summary>
		/// 設定一時ファイル名
		/// </summary>
		public static readonly string SettingsTempFileName = "Hanbura.xml.tmp";

		/// <summary>
		/// プラグインディレクトリのパス
		/// </summary>
		public static readonly string PluginsDirectory = "Plugins";

		/// <summary>
		/// プラグイン定義ファイルのパス
		/// </summary>
		public static readonly string PluginDefFileExt = ".json";

		/// <summary>
		/// ゲームプラグインディレクトリのパス
		/// </summary>
		public static readonly string GameDirectory = "Games";

		/// <summary>
		/// ゲームプラグイン定義ファイルの拡張子
		/// </summary>
		public static readonly string GameDefFileName = "GameDef.json";
	}
}
