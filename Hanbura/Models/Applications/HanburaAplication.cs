using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Hanbura.Views;

namespace Studiotaiha.Hanbura.Models.Applications
{
	internal class HanburaAplication : IApplication
	{
		public void OnStartup(App app, StartupOptions options, IAlertManager alertManager, ILogger logger)
		{
			app.MainWindow = new MainWindow();
			app.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
			app.MainWindow.Show();
		}

		public void OnShutdown(App app)
		{
		}
	}
}
