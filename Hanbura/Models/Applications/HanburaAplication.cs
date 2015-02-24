using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Hanbura.Models.Common.Alert;
using Studiotaiha.Hanbura.Models.Common.Settings;
using Studiotaiha.Hanbura.ViewModels;
using Studiotaiha.Hanbura.Views;

namespace Studiotaiha.Hanbura.Models.Applications
{
	internal class HanburaAplication : IApplication
	{
		Settings settings_;
		SettingsAutoExpoter settingsAutoExpoter_;
		IDisposable clearCaptionMessageSubscription_;

		public void OnStartup(App app, StartupOptions options, IAlertManager alertManager, ILogger logger)
		{
			// 設定初期化
			logger.Information("設定を初期化します。");
			InitializeSettinsg(logger);

			// メインウィンドウ初期化
			logger.Information("メインウィンドウを生成します。");
			var window = new MainWindow();
			var vm = new MainWindowViewModel(new WPFDispatcher(window.Dispatcher));
			window.DataContext = vm;

			// キャプションバーメッセージ変更時の処理
			var alertManagerImpl = alertManager as AlertManager;
			if (alertManagerImpl != null) {
				// 設定する方
				alertManagerImpl.CaptionMessageChanged += (_, e) => {
					vm.CaptionMessage = e.Messsage;
					vm.CaptionMessageKind = e.Kind;
				};

				SetNotifiactionClearEvent(vm, alertManagerImpl);
				settings_.PropertyChanged += (_, e) => {
					if (e.PropertyName == settings_.GetMemberName(() => settings_.CaptionMessageClearDelay)) {
						SetNotifiactionClearEvent(vm, alertManagerImpl);
					}
				};
			}

			// メインウィンドウ表示
			app.MainWindow = window;
			app.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
			app.MainWindow.Show();

			logger.Information("Hanbura Application起動処理完了");
		}

		public void OnShutdown(App app, ILogger logger)
		{
			// 設定自動保存を停止
			if (settingsAutoExpoter_ != null) {
				logger.Information("設定自動保存を停止します。");
				settingsAutoExpoter_.Dispose();
				settingsAutoExpoter_ = null;
			}

			if(clearCaptionMessageSubscription_ != null){
				clearCaptionMessageSubscription_.Dispose();
				clearCaptionMessageSubscription_ = null;
			}

			logger.Information("Hanbura Application終了処理完了");
		}

		void SetNotifiactionClearEvent(MainWindowViewModel vm, AlertManager alertManager)
		{
			if (clearCaptionMessageSubscription_ != null) {
				clearCaptionMessageSubscription_.Dispose();
				clearCaptionMessageSubscription_ = null;
			}

			clearCaptionMessageSubscription_ = Observable.FromEventPattern<CaptionMessageChangedEventArgs>(alertManager, "CaptionMessageChanged")
				.Throttle(TimeSpan.FromSeconds(settings_.CaptionMessageClearDelay))
				.Subscribe(args => {
					vm.CaptionMessage = "";
					vm.CaptionMessageKind = CaptionMessageKind.None;
				});
		}

		/// <summary>
		/// 設定を初期化する。
		/// </summary>
		/// <param name="logger"></param>
		void InitializeSettinsg(ILogger logger)
		{
			var settingsImpl = new SettingsImpl("Hanbura", Settings.KnownTypes);
			settings_ = new Settings(settingsImpl, new WPFDispatcher(App.Current.Dispatcher));
			settingsAutoExpoter_ = new SettingsAutoExpoter(
				Path.Combine(AppInfo.Current.StartupDirectory, Constants.SettingsFileName),
				Path.Combine(AppInfo.Current.StartupDirectory, Constants.SettingsTempFileName),
				settingsImpl,
				new DataContractSettingsSerializer(),
				300);
			settingsAutoExpoter_.Error += (_, args) => {
				logger.Error("設定の保存に失敗しました。", args.GetException());
			};
			settingsAutoExpoter_.Exported += (_, __) => {
				logger.Verbose("設定を保存しました。");
			};
		}
	}
}
