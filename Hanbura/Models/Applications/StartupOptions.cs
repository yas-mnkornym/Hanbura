using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;

namespace Studiotaiha.Hanbura.Models.Applications
{
	/// <summary>
	/// コマンドラインオプション
	/// </summary>
	internal class StartupOptions
	{
		#region Updating
		/// <summary>
		/// trueならアップデートモードでアプリケーションを起動する。
		/// </summary>
		[Option("Update", DefaultValue = false)]
		public bool UpdateMode { get; set; }

		/// <summary>
		/// アップデート処理に利用する作業ディレクトリ
		/// </summary>
		[Option("UpdateWorkingDir")]
		public string UpdateWorkingDir { get; set; }

		/// <summary>
		/// アップデート後に起動すべき実行ファイルパス
		/// </summary>
		[Option("ExePath")]
		public string ExePath { get; set; }

		/// <summary>
		/// アプリケーションが格納されているディレクトリのパス
		/// </summary>
		[Option("AppDir")]
		public string AppDir { get; set; }

		/// <summary>
		/// アップデータのファイルパス
		/// </summary>
		[Option("UpdateFile")]
		public string UpdateFilePath { get; set; }

		/// <summary>
		/// ターゲットバージョン
		/// </summary>
		[Option("TargetVersion")]
		public string TargetVersion { get; set; }
		#endregion // Updating
	}
}
