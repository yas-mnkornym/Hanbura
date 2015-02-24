using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Hanbura.Models;

namespace Studiotaiha.Hanbura.ViewModels
{
	internal class MainWindowViewModel : BindableBase
	{
		public MainWindowViewModel(IDispatcher dispatcher)
			: base(dispatcher)
		{ }
		
		#region Bindings
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

		#region WindowSettings
		WindowSettings windowSettings_;
		public WindowSettings WindowSettings
		{
			get
			{
				return windowSettings_;
			}
			set
			{
				SetValue(ref windowSettings_, value);
			}
		}
		#endregion

		#region SettingsControlVm
		SettingsControlViewModel settingsControlVm_;
		public SettingsControlViewModel SettingsControlVm
		{
			get
			{
				return settingsControlVm_;
			}
			set
			{
				SetValue(ref settingsControlVm_, value);
			}
		}
		#endregion

		#region Caption Messages
		#region NotificationMessage
		string captionMessage_;
		public string CaptionMessage
		{
			get
			{
				return captionMessage_;
			}
			set
			{
				SetValue(ref captionMessage_, value);
			}
		}
		#endregion

		#region CaptionMessageKind
		CaptionMessageKind captionMessageKind_ = CaptionMessageKind.None;
		public CaptionMessageKind CaptionMessageKind
		{
			get
			{
				return captionMessageKind_;
			}
			set
			{
				SetValue(ref captionMessageKind_, value);
			}
		}
		#endregion
		#endregion // Caption Messages
		#endregion // Bindings
	}
}
