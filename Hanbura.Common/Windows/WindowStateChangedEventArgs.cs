using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Windows
{
	public class WindowStateChangedEventArgs : EventArgs
	{
		public WindowStateChangedEventArgs(
			EWindowState state)
		{
			State = state;
		}

		public EWindowState State { get; private set; }
	}
}
