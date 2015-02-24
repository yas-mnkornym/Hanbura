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
	}
}
