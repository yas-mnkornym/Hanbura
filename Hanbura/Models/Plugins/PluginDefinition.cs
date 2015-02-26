using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Models.Plugins
{
	public class PluginDefinition
	{
		/// <summary>
		/// 対応しているゲームのIDを取得する
		/// </summary>
		/// <remarks>nullの時，全てのゲームに対応していると判断する．</remarks>
		public string[] CompatibleGameIds { get; set;}
	}
}
