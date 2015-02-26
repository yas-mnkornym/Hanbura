using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Codeplex.Data;
using Studiotaiha.Hanbura.Models.Common.Alert;
using Studiotaiha.Hanbura.Models.Common.Settings;
using Studiotaiha.Hanbura.Models.Plugins;
using Studiotaiha.Hanbura.Models.Windows;
using Studiotaiha.Hanbura.ViewModels;
using Studiotaiha.Hanbura.Views;

namespace Studiotaiha.Hanbura.Models.Applications
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
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
			var windowSettings = settings_.GetWindowSettings("MainWindow");
			var vm = new MainWindowViewModel(new WPFDispatcher(App.Current.Dispatcher)) {
				Settings = settings_,
				WindowSettings = windowSettings,
				SettingsControlVm = new SettingsControlViewModel {
					Settings = settings_,
					MainWindowSettings = windowSettings
				}
			};
			var window = new MainWindow() {
				DataContext = vm
			};

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


			// TODO: 最大化時の画面枠を調整する
			// TODO: ウィンドウのスナップをオン／オフできるようにする
			// TODO: 子ウィンドウの設定画面を作る
			var windowManager = new WindowManager(settings_, new WPFDispatcher(App.Current.Dispatcher));
			var childWindow = windowManager.CreateWindow("Test", new Hanbura.Windows.WindowConfig {
				IsResizable = true,
				Caption = "うんこ"
			});

			childWindow.Creating += (_, e) => {
				var button = new Button { Content = "ちんこ" };
				button.Click += async (__, ___) => {
					childWindow.Hide();
					await Task.Delay(1000);
					childWindow.Show();
				};
				e.Content = button;
				e.SettingsContent = "まんこ";
			};
			childWindow.Show();

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
			var serializer = new DataContractSettingsSerializer();
			
			// 設定を読込
			try {
				var settingsFilePath = Path.Combine(AppInfo.Current.StartupDirectory, Constants.SettingsFileName);
				if (File.Exists(settingsFilePath)) {
					logger.Information("設定を読み込みます。");
					using (var fs = new FileStream(settingsFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
						serializer.Deserialize(fs, settingsImpl);
					}
				}
				else {
					logger.Information("設定ファイルが見つかりませんでした。既定の設定を利用します。");
				}
			}
			catch (Exception ex) {
				logger.Error("設定の読込に失敗しました。", ex);
				AlertService.Current.AlertManager.ShowErrorMessage("設定の読込に失敗しました。\nデフォルトの設定を利用します。", ex);
			}

			settingsAutoExpoter_ = new SettingsAutoExpoter(
				Path.Combine(AppInfo.Current.StartupDirectory, Constants.SettingsFileName),
				Path.Combine(AppInfo.Current.StartupDirectory, Constants.SettingsTempFileName),
				settingsImpl, serializer, 300);
			settingsAutoExpoter_.Error += (_, args) => {
				logger.Error("設定の保存に失敗しました。", args.GetException());
			};
			settingsAutoExpoter_.Exported += (_, __) => {
				logger.Verbose("設定を保存しました。");
			};
		}
	}
}
