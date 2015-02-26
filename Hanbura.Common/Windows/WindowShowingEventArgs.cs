using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Windows
{
	public class WindowShowingEventArgs : EventArgs
	{
		public WindowShowingEventArgs(
			IDispatcher uiDispatcher,
			object content = null,
			object settingsContent = null)
		{
			UIDispatcher = uiDispatcher;
			Content = content;
			SettingsContent = settingsContent;
		}

		public IDispatcher UIDispatcher { get; private set; }
		public object Content { get; set; }
		public object SettingsContent { get; set; }
	}
}
