using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Hanbura.Plugins;

namespace Studiotaiha.Hanbura.Models.Plugins
{
	internal class PluginLoader
	{
		/// <summary>
		/// ファイルを指定してプラグインをロードする
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <returns>ファイルからロードしたプラグイン</returns>
		public IEnumerable<IPlugin> LoadFromFile(string filePath)
		{
			return LoadFromFile<IPlugin>(filePath);
		}

		/// <summary>
		/// ファイルを指定して特定の型のプラグインをロードする
		/// </summary>
		/// <typeparam name="TPlugin">プラグインの型</typeparam>
		/// <param name="filePath">ファイルパス</param>
		/// <returns>ファイルからロードしたプラグイン</returns>
		public IEnumerable<TPlugin> LoadFromFile<TPlugin>(string filePath)
			where TPlugin : IPlugin
		{
			if (filePath == null) { throw new ArgumentNullException("filePath"); }
			Assembly asm = null;
			try {
				Assembly.LoadFrom(filePath);
			}
			catch (BadImageFormatException) { }
			catch (FileNotFoundException) { }

			// ロードできなかったら空のコレクションを返す
			if (asm == null) { return new TPlugin[] { }; }

			// ロードできたらプラグインのインスタンスを返す
			return asm.GetTypes()
				.Where(type => type != null)
				.Where(type => (type.IsClass && type.IsPublic && !type.IsAbstract &&
					type.GetInterface(typeof(TPlugin).FullName) != null))
				.Select(type => (TPlugin)Activator.CreateInstance(type))
				.Where(plugin => plugin != null);
		}
	}
}
