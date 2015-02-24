using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Hanbura.Models;

namespace Studiotaiha.Hanbura.ViewModels
{
	internal class SettingsControlViewModel : BindableBase
	{
		#region Settings
		Settings settings_;
		public Settings Settings
		{
			get
			{
				return settings_;
			}
			set
			{
				SetValue(ref settings_, value);
			}
		}
		#endregion

		#region MainWindowSettings
		WindowSettings mainWindowSettings_;
		public WindowSettings MainWindowSettings
		{
			get
			{
				return mainWindowSettings_;
			}
			set
			{
				SetValue(ref mainWindowSettings_, value);
			}
		}
		#endregion
	}
}
