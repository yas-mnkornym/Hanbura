using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Plugins
{
	/// <summary>
	/// プラグインに与えられる情報
	/// </summary>
	public interface IPluginInfo
	{
		/// <summary>
		/// プラグイン用のロガーを取得する
		/// </summary>
		ILogger Logger { get; }

		/// <summary>
		/// アラートマネージャを取得する
		/// </summary>
		IAlertManager AlertManager { get; }

		/// <summary>
		/// プラグイン用の設定を取得する。
		/// </summary>
		/// <param name="knownTypes">設定をデシリアライズするための既知の型情報</param>
		/// <returns>プラグイン用の設定インスタンス</returns>
		/// <remarks>複数回呼び出しても、同じインスタンスが返される。</remarks>
		ISettings GetSettings(IEnumerable<Type> knownTypes = null);

		/// <summary>
		/// 汎ブラのバージョンを取得する
		/// </summary>
		Version HanburaVersion { get; }
	}
}
