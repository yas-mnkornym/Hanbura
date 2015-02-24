using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Studiotaiha.Hanbura.Models
{
	internal class Settings : SettingsBase
	{
		public static readonly Type[] KnownTypes = new Type[]{
			typeof(Color)
		};

		public Settings(ISettings settings, IDispatcher dispatcher)
			: base(settings, dispatcher)
		{ }

		#region 表示設定
		/// <summary>
		/// 処理中画面背景色
		/// </summary>
		public Color WorkingBackgroundColor
		{
			get
			{
				return GetMe(Colors.DarkBlue);
			}
			set
			{
				SetMe(value);
			}
		}
		#endregion 

		#region ウィンドウ設定
		/// <summary>
		/// ウィンドウ設定を取得する。
		/// </summary>
		/// <param name="tag">設定タグ</param>
		/// <returns>ウィンドウ設定</returns>
		public WindowSettings GetWindowSettings(string tag)
		{
			var actualTag = string.Format("WindowSettings_{0}", tag);
			var childSettings = Settings.GetChildSettings(actualTag, WindowSettings.KnownTypes);
			return new WindowSettings(childSettings, Dispatcher);
		}
		#endregion
	}
}
