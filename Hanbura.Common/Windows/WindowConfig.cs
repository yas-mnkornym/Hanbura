using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Studiotaiha.Hanbura.Windows
{
	public sealed class WindowConfig
	{
		public WindowConfig()
		{
			TopMost = null;
			IsResizable = true;
			DefaultWidth = 800;
			DefaultHeight = 600;
			DefaultLeft = double.NaN;
			DefaultTop = double.NaN;
			AllowsTransparency = false;
			Opacity = null;
			CanBeMaximized = true;
			CanBeMinimized = true;
			RestorePosition = true;
			RestoreSize = true;
			Caption = null;
		}

		/// <summary>
		/// ウィンドウのキャプションを取得・設定する
		/// </summary>
		public string Caption { get; set; }

		/// <summary>
		/// 最前面に表示するかどうかを示す値を取得・設定する
		/// </summary>
		public bool? TopMost { get; set; }


		/// <summary>
		/// ウィンドウが手動でリサイズ可能かどうかを指定する
		/// </summary>
		public bool IsResizable { get; set; }

		/// <summary>
		/// ウィンドウのデフォルト幅を指定する
		/// </summary>
		/// <remarks>IsResizableがtrueの時のみ使用される</remarks>
		public double DefaultWidth { get; set; }

		/// <summary>
		/// ウィンドウのデフォルト高さを指定する。
		/// </summary>
		/// <remarks>IsResizableがtrueの時のみ使用される</remarks>
		public double DefaultHeight { get; set; }

		/// <summary>
		/// ウィンドウのデフォルト上端座標を取得・設定する
		/// </summary>
		public double DefaultLeft { get; set; }

		/// <summary>
		/// ウィンドウのデフォルト上端座標を取得・設定する
		/// </summary>
		public double DefaultTop { get; set; }

		/// <summary>
		/// オーナウィンドウから独立可能であるかどうかを示す値を取得・設定する
		/// </summary>
		public bool CanBeIndependentFromOwner { get; set; }

		/// <summary>
		/// ウィンドウの透過表示を許可するかどうかを指定する
		/// </summary>
		/// <remarks>WebBrowserコントロール等のWindow.AllowsTransparncy=Trueで動作しないコントロール利用時に注意すること。</remarks>
		public bool AllowsTransparency { get; set; }

		/// <summary>
		/// ウィンドウの不透明度を取得・設定する
		/// </summary>
		public double? Opacity { get; set; }

		/// <summary>
		/// ウィンドウが最小化可能であるかどうかを示す値を取得・設定する
		/// </summary>
		public bool CanBeMinimized { get; set; }

		/// <summary>
		/// ウィンドウが最大化可能であるかどうかを示す値を取得・設定する
		/// </summary>
		public bool CanBeMaximized { get; set; }

		/// <summary>
		/// trueの場合、閉じるボタンが押された時、Close()せずにHide()を行う
		/// </summary>
		public bool HidOnClose{get;set;}

		/// <summary>
		/// 位置を自動で復元するかどうかを示す値を取得・設定する
		/// </summary>
		public bool RestorePosition { get; set; }

		/// <summary>
		/// サイズを自動で復元するかどうかを示す値を取得・設定する
		/// </summary>
		/// <remarks>IsResizableがfalseの時には無視される。</remarks>
		public bool RestoreSize { get; set; }
	}
}
