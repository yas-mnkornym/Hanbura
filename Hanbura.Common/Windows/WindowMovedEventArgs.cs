using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Windows
{
	public class WindowMovedEventArgs : EventArgs
	{
		public WindowMovedEventArgs(
			double left,
			double top,
			double oldLeft,
			double oldTop)
		{
			Left = left;
			Top = top;
			OldLeft = oldLeft;
			OldTop = oldTop;
		}

		public double Left { get; private set; }
		public double Top { get; private set; }
		public double OldLeft { get; private set; }
		public double OldTop { get; private set; }
		public double LeftDifference { get { return (Left - OldLeft); } }
		public double TopDifference { get { return (Top - OldTop); } }
		public bool LeftChanged { get { return (Left != OldLeft); } }
		public bool TopChanged { get { return (Top != OldTop); } }
	}
}
