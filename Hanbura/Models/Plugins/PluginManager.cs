using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codeplex.Data;
using Studiotaiha.Hanbura.Plugins;

namespace Studiotaiha.Hanbura.Models.Plugins
{
	public class PluginManager
	{
		static ILogger logger_ = null;
		static ILogger Logger
		{
			get
			{
				return logger_ ?? (logger_ = LoggingService.Current.GetLogger("PluginManager"));
			}
		}

		List<IPlugin> plugins_ = new List<IPlugin>();

		/// <summary>
		/// 登録されているプラグインを取得する
		/// </summary>
		IEnumerable<IPlugin> Plugins { get { return plugins_; } }

		/// <summary>
		/// 特定の型のプラグインを取得する
		/// </summary>
		/// <typeparam name="T">取得するプラグインの型</typeparam>
		/// <returns>指定した型のプラグインの列挙子</returns>
		IEnumerable<T> GetPlugins<T>()
			where T : IPlugin
		{
			return Plugins.Where(plugin => plugin is T).Cast<T>();
		}

		/// <summary>
		/// 特定の型で、かつ条件に一致するプラグインを取得する
		/// </summary>
		/// <typeparam name="T">取得するプラグインの型</typeparam>
		/// <param name="predicate">条件を表す述語</param>
		/// <returns>指定した型・条件に一致するプラグインの列挙子</returns>
		IEnumerable<T> GetPlugins<T>(Func<IPlugin, bool> predicate)
			where T : IPlugin
		{
			return GetPlugins<T>().Where(plugin => predicate(plugin));
		}

		void RegisterPlugin(IPlugin plugin)
		{
		}

		IGamePlugin LoadGame(GamePluginDefinition def)
		{
			// ゲームプラグインをロード
			var loader = new PluginLoader();
			var gameId = def.GameId;
			var pluginFile = Path.Combine(def.Directory, def.Filename);
			var gamePlugin = loader.LoadFromFile<IGamePlugin>(pluginFile)
				.FirstOrDefault(x => x.GameID == gameId);
			if (gamePlugin == null) { throw new GamePluginLoadException(gameId, pluginFile); }
			if (gamePlugin.GameID == null) { throw new InvalidPluginException(pluginFile, string.Format("ゲームプラグインが不正なGameIDを返しました。")); }

			// ゲームに対応したプラグインをロード
			var plugins = LoadNonGamePluginsFromDirectory(Path.Combine(AppInfo.Current.StartupDirectory, Constants.PluginsDirectory), gamePlugin.GameID);

			return gamePlugin;
		}

		/// <summary>
		/// 指定したゲームIDに対応したプラグインをロードする。
		/// </summary>
		/// <param name="dirPath">プラグインを捜索するディレクトリ</param>
		/// <param name="gameId">ゲームID(nullを指定すると全てのプラグインをロードする)</param>
		/// <returns>ロードされたプラグインのインスタンス</returns>
		public static IEnumerable<IPlugin> LoadNonGamePluginsFromDirectory(string dirPath, string gameId = null)
		{
			if (dirPath == null) { throw new ArgumentNullException("dirPath"); }
			if (!Directory.Exists(dirPath)) { return new IPlugin[] { }; }

			var loader = new PluginLoader();
			return Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories)
				.SelectMany(file => {
					var dir = Path.GetDirectoryName(file);
					if (gameId != null) {
						var defFilePath = Path.Combine(dir, string.Format("{0}{1]", Path.GetFileNameWithoutExtension(file), Constants.PluginDefFileExt));
						if (File.Exists(defFilePath)) {
							try {
								var def = (PluginDefinition)DynamicJson.Parse(File.ReadAllText(defFilePath, Encoding.UTF8));
								if (def.CompatibleGameIds != null && !def.CompatibleGameIds.Contains(gameId)) { return null; }
							}
							catch (Exception ex) {
								Logger.Error(string.Format("プラグイン定義ファイルの読み込みに失敗しました。(ファイル:{0})", defFilePath), ex);
							}
						}
					}
					var plugins = loader.LoadFromFile<INonGamePlugin>(file);
					if (gameId != null) {
						plugins = plugins.Where(x => x.IsCompatibleWith(gameId));
					}
					return plugins;
				})
				.Where(plugin => plugin != null);
		}

		/// <summary>
		/// ゲームプラグインの定義ファイルを読み込む
		/// </summary>
		/// <param name="gameDirPath"></param>
		/// <returns></returns>
		public static IEnumerable<GamePluginDefinition> LoadGamePluginDefinitions(string gameDirPath)
		{
			if (gameDirPath == null) { throw new ArgumentNullException("directory"); }
			if (!Directory.Exists(gameDirPath)) { return null; }

			var files = Directory.GetFiles(gameDirPath, Constants.GameDefFileName, SearchOption.AllDirectories);
			return files.Select(file => {
				try {
					var def = (GamePluginDefinition)DynamicJson.Parse(file);
					def.Directory = Path.GetDirectoryName(file);
					return def;
				}
				catch (Exception ex) {
					Logger.Error(string.Format("ゲームプラグイン定義ファイルの読み込みに失敗しました。(ファイル:{0})", file), ex);
					return null;
				}
			})
			.Where(def => def != null);
		}
	}

	[Serializable]
	public class PluginException : Exception
	{
		public PluginException() { }
		public PluginException(string message) : base(message) { }
		public PluginException(string message, Exception inner) : base(message, inner) { }
		protected PluginException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}

	[Serializable]
	public class InvalidPluginException : PluginException
	{
		public InvalidPluginException(string pluginFileName, string message)
			: base(string.Format("{0}(ファイル:{1})", message, pluginFileName))
		{ }

		protected InvalidPluginException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}

	[Serializable]
	public class PluginLoadException : PluginException
	{
		public PluginLoadException(string pluginFileName)
			: this(pluginFileName, "プラグインのロードに失敗しました。")
		{}
		
		public PluginLoadException(string pluginFileName, string message) 
			: base(string.Format("{1}(プラグインファイル名: {0})", pluginFileName, message))
		{}
		
		public PluginLoadException(string pluginFileName, Exception inner)
			: this(pluginFileName, "プラグインのロードに失敗しました。", inner)
		{}
		
		public PluginLoadException(string pluginFileName, string message, Exception inner) 
			: base(string.Format("{1}(プラグインファイル名: {0})", pluginFileName, message), inner)
		{}
		
		protected PluginLoadException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}

	[Serializable]
	public class GamePluginLoadException : PluginLoadException
	{
		public GamePluginLoadException(string gameId, string pluginFileName) 
			: base(pluginFileName, string.Format("ゲームプラグインのロードに失敗しました。(ゲームID:{0})", gameId))
		{}

		protected GamePluginLoadException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
