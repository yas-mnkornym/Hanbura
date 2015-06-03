using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Windows
{
	public class WindowResizedEventArgs : EventArgs
	{
		public WindowResizedEventArgs(
			double width,
			double height,
			double oldWidth,
			double oldHeight)
		{
			Width = width;
			Height = height;
			OldWidth = oldWidth;
			OldHeight = oldHeight;
		}

		public double Width { get; private set; }
		public double Height { get; private set; }
		public double OldWidth { get; private set; }
		public double OldHeight { get; private set; }

		public double WidthDifference { get { return (Width - OldWidth); } }
		public double HeightDifference { get { return (Height - OldHeight); } }
		public bool WidthChanged { get { return (Width != OldWidth); } }
		public bool HeightChanged { get { return (Height != OldHeight); } }
	}

}
