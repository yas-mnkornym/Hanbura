using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Hanbura.Models.Common.Settings;
using Studiotaiha.Hanbura.Views;

namespace Studiotaiha.Hanbura.Models.Applications
{
	internal class HanburaAplication : IApplication
	{
		Settings settings_;
		SettingsAutoExpoter settingsAutoExpoter_;

		public void OnStartup(App app, StartupOptions options, IAlertManager alertManager, ILogger logger)
		{
			// 設定初期化
			logger.Information("設定を初期化します。");
			InitializeSettinsg(logger);

			// メインウィンドウ初期化
			logger.Information("メインウィンドウを生成します。");
			app.MainWindow = new MainWindow();
			app.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;

			// メインウィンドウ表示
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

			logger.Information("Hanbura Application終了処理完了");
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
