using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Hanbura.Properties;
using Studiotaiha.Hanbura.ViewModels.Alerts;
using Studiotaiha.Hanbura.Views.Alerts;

namespace Studiotaiha.Hanbura.Models.Common.Alert
{
	internal class AlertManager : Dispatchable, IAlertManager
	{
		public AlertManager(IDispatcher dispatcher)
			: base(dispatcher)
		{ }

		bool IsNullOrEmpty<T>(IEnumerable<T> sequence)
		{
			return (sequence == null || !sequence.Any());
		}

		public string ShowMessage(string message, string caption, Exception exception, EAlertType alertType, params string[] selections)
		{
			return Dispatch(() => {
				var vm = new StringAlertWindowViewModel(
					caption,
					message,
					alertType,
					IsNullOrEmpty(selections) ? new string[] { Resources.Ok } : selections,
					exception,
					Dispatcher);
				var window = new AlertWindow { DataContext = vm };
				string selection = null;
				vm.Selected += (_, s) => {
					selection = s;
					window.Close();
				};
				window.ShowDialog();
				return selection;
			});
		}

		public void PostMessage(string message, string caption, Exception exception, EAlertType alertType, Action<string> onResult, params string[] selections)
		{
			Dispatch(() => {
				var vm = new StringAlertWindowViewModel(
					caption,
					message,
					alertType,
					IsNullOrEmpty(selections) ? new string[] { Resources.Ok } : selections,
					exception,
					Dispatcher);
				var window = new AlertWindow { DataContext = vm };
				string selection = null;
				vm.Selected += (_, s) => {
					selection = s;
					window.Close();
				};
				window.Closed += (_, __) => {
					if (onResult != null) {
						onResult(selection);
					}
				};
				window.Show();

			});
		}

		public string ShowDialog(Action<IDialogConfig> initializer, EAlertType alertType, params string[] selections)
		{
			return Dispatch(() => {
				// 初期化
				var config = new DialogConfig {
					Dispatcher = Dispatcher,
					Selections = selections
				};
				initializer(config);

				// ウィンドウを表示
				var vm = new ContentAlertWindowViewModel(
					config.Caption ?? Resources.Dialog,
					config.Content,
					alertType,
					IsNullOrEmpty(config.Selections) ? new string[] { Resources.Ok } : config.Selections,
					config.Callback,
					Dispatcher);
				var window = new AlertWindow { DataContext = vm };
				string selection = null;
				vm.Selected += (_, s) => {
					selection = s;
					if (config.Callback != null) {
						if (!config.Callback.OnClosing(selection)) {
							return;
						}
					}
					window.Close();
				};

				window.Closed += (_, __) => {
					if (config.Callback != null) {
						config.Callback.OnClosed();
					}
				};

				window.ShowDialog();
				return selection;
			});
		}

		public void PostDialog(Action<IDialogConfig> initializer, EAlertType alertType, Action<string> onResult, params string[] selections)
		{
			Dispatch(() => {
				// 初期化
				var config = new DialogConfig {
					Dispatcher = Dispatcher,
					Selections = selections
				};
				initializer(config);

				// ウィンドウを表示
				var vm = new ContentAlertWindowViewModel(
					config.Caption ?? Resources.Dialog,
					config.Content,
					alertType,
					IsNullOrEmpty(config.Selections) ? new string[] { Resources.Ok } : config.Selections,
					config.Callback,
					Dispatcher);
				var window = new AlertWindow { DataContext = vm };
				string selection = null;
				vm.Selected += (_, s) => {
					selection = s;
					if (config.Callback != null) {
						if (!config.Callback.OnClosing(selection)) {
							return;
						}
					}
					window.Close();
				};

				window.Closed += (_, __) => {
					if (config.Callback != null) {
						config.Callback.OnClosed();
					}
					if (onResult != null) {
						onResult(selection);
					}
				};

				window.Show();
			});
		}

		public void SetCaptionMessage(string message, CaptionMessageKind kind = CaptionMessageKind.Information)
		{
			if (CaptionMessageChanged != null) {
				CaptionMessageChanged(this, new CaptionMessageChangedEventArgs(message, kind));
			}
		}

		public event EventHandler<CaptionMessageChangedEventArgs> CaptionMessageChanged;
	}
}
