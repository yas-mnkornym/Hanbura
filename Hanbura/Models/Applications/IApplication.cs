using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Models.Applications
{
	internal interface IApplication
	{
		void OnStartup(
			App app,
			StartupOptions options,
			IAlertManager alertManager,
			ILogger logger);

		void OnShutdown(App app);

	}
	 
}
