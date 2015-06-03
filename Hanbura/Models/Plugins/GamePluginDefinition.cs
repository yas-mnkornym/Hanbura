using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Models.Plugins
{
	public class GamePluginDefinition
	{
		/// <summary>
		/// ゲームタイトルを取得・設定する
		/// </summary>
		public string GameTitle { get; set; }

		/// <summary>
		/// ゲームIDを取得・設定する
		/// </summary>
		public string GameId { get; set; }

		/// <summary>
		/// ゲームのURLを取得・設定する
		/// </summary>
		public string GameUrl { get; set; }

		/// <summary>
		/// プラグインの拡張子付きファイル名を取得する。
		/// </summary>
		public string Filename { get; set; }

		/// <summary>
		/// プラグインの作者名を表す取得・設定する
		/// </summary>
		public string Author { get; set; }

		/// <summary>
		/// プラグインの作者を代表するWebサイトのURLを取得・設定する
		/// </summary>
		public string AuthorUrl { get; set; }

		/// <summary>
		/// ゲームプラグインのサムネイルファイル名を取得・設定する
		/// </summary>
		public string Thumbnail { get; set; }

		/// <summary>
		/// 格納しているディレクトリのパス
		/// </summary>
		public string Directory { get; set; }
	}
}
