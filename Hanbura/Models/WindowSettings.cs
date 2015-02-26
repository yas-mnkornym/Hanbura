using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Studiotaiha.Hanbura.Models
{
	/// <summary>
	/// ウィンドウの設定
	/// </summary>
	public class WindowSettings : SettingsBase
	{
		public static readonly Type[] KnownTypes = new Type[] { typeof(WindowState) };

		public WindowSettings(
			ISettings settings,
			IDispatcher dispatcher)
			: base(settings, dispatcher)
		{ }

		/// <summary>
		/// 常に手前に表示する
		/// </summary>
		public bool AlwaysOnTop{
			get{
				return GetMe(false);
			}
			set{
				SetMe(value);
			}
		}

		/// <summary>
		/// 前回終了時の位置を復元するかどうかを示すフラグ
		/// </summary>
		public bool RestorePosition
		{
			get
			{
				return GetMe(true);
			}
			set
			{
				SetMe(value);
			}
		}

		/// <summary>
		/// 前回終了時のサイズを復元するかどうかを示すフラグ
		/// </summary>
		public bool RestoreSize
		{
			get
			{
				return GetMe(true);
			}
			set
			{
				SetMe(value);
			}
		}

		/// <summary>
		/// ウィンドウ位置のX座標
		/// </summary>
		public double Left
		{
			get
			{
				return GetMe(double.NaN);
			}
			set
			{
				SetMe(value);
			}
		}

		/// <summary>
		/// ウィンドウ位置のY座標
		/// </summary>
		public double Top
		{
			get
			{
				return GetMe(double.NaN);
			}
			set
			{
				SetMe(value);
			}
		}

		/// <summary>
		/// ウィンドウの高さ
		/// </summary>
		public double Width
		{
			get
			{
				return GetMe(double.NaN);
			}
			set
			{
				SetMe(value);
			}
		}

		/// <summary>
		/// ウィンドウの幅
		/// </summary>
		public double Height
		{
			get
			{
				return GetMe(double.NaN);
			}
			set
			{
				SetMe(value);
			}
		}

		/// <summary>
		/// ウィンドウの状態
		/// </summary>
		public WindowState State
		{
			get
			{
				return GetMe(WindowState.Normal);
			}
			set
			{
				SetMe(value);
			}
		}


		/// <summary>
		/// 親ウィンドウから独立しているかどうか
		/// </summary>
		/// <remarks>MainWindowでは利用しない</remarks>
		public bool IsIndependentFromOwner
		{
			get
			{
				return GetMe(false);
			}
			set
			{
				SetMe(value);
			}
		}

		/// <summary>
		/// ウィンドウを他のウィンドウにスナップするかどうか
		/// </summary>
		public bool EnableSnapping
		{
			get
			{
				return GetMe(true);
			}
			set
			{
				SetMe(value);
			}
		}

		public bool TrackOwner{
			get{
				return GetMe(true);
			}
			set{
				SetMe(value);
			}
		}

		public double Opacity
		{
			get
			{
				return GetMe(1.0);
			}
			set
			{
				SetMe(value);
			}
		}
	}
}
