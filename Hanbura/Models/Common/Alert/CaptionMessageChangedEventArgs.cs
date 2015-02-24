using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Models.Common.Alert
{
	class CaptionMessageChangedEventArgs
	{
		public CaptionMessageChangedEventArgs(
			string message,
			CaptionMessageKind kind)
		{
			Messsage = message;
			Kind = kind;
		}

		public string Messsage { get; private set; }
		public CaptionMessageKind Kind { get; private set; }
	}
}
