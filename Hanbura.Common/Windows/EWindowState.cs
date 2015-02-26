using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Windows
{
	public enum EWindowState
	{
		/// <summary>
		/// 非表示状態
		/// </summary>
		Hidden = 0,

		/// <summary>
		/// 通常（ウィンドウ表示）
		/// </summary>
		Normal = 1,

		/// <summary>
		/// 最大化状態
		/// </summary>
		Maximized = 2,

		/// <summary>
		/// 最小化状態
		/// </summary>
		Minimized = 3,

		/// <summary>
		/// ウィンドウが閉じられている
		/// </summary>
		Closed = 4
	}
}
