
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Models.Alert
{
	internal sealed class DialogConfig : IDialogConfig
	{
		public IDispatcher Dispatcher { get; set; }

		public object Content { get; set; }

		public string Caption { get; set; }

		public IDialogCallback Callback { get; set; }

		public IEnumerable<string> Selections { get; set; }
	}
}
