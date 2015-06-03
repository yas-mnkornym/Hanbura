using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Plugins
{
	/// <summary>
	/// ゲームプラグインではないプラグイン
	/// </summary>
	public interface INonGamePlugin : IPlugin
	{
		/// <summary>
		/// ゲームに対応しているかどうかを判定する。
		/// </summary>
		/// <param name="gameId">ゲームに対応しているかを判定する。</param>
		/// <returns>対応していればtrueを、対応していなければfalseを返す。</returns>
		bool IsCompatibleWith(string gameId);
	}
}
