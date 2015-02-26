using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Models.Game
{
	public class ProxySettings
	{
		public string Host { get; set; }

		public int Port { get; set; }

		public Func<ProxySettings, string> StringConverter { get; set; }

		public override string ToString()
		{
			if (StringConverter != null) {
				return StringConverter(this);
			}
			else {
				return string.Format("{0}:{1}", Host, Port);
			}
		}
	}
}
