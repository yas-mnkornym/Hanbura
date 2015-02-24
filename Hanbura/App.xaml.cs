using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows;
using CommandLine;
using Studiotaiha.Hanbura.Models.Common.Alert;
using Studiotaiha.Hanbura.Models.Applications;
using Studiotaiha.Hanbura.Models.Common.Logging;

namespace Studiotaiha.Hanbura
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public partial class App : Application
	{
		ILogger logger_;
		IAlertManager alertManager_;
		IApplication application_;
		CompositeDisposable disposables_ = new CompositeDisposable();

		void InitializeLogger()
		{
			logger_ = new Logger("Hanbura");
			var logExpoter = new LogExpoter(logger_,
				Path.Combine(AppInfo.Current.StartupDirectory, Constants.LogFileName));
			disposables_.Add(logExpoter);
			LoggingService.Initialize(logger_);
		}

		void InitializeAlert()
		{
			alertManager_ = new AlertManager(new WPFDispatcher(Dispatcher));
			AlertService.Initialize(alertManager_);
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			DispatcherUnhandledException += App_DispatcherUnhandledException;
			ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;

			// ロガーを初期化
			InitializeLogger();
			logger_.Information("アプリケーションの初期化を開始します。");

			// アラートサービスを初期化
			logger_.Information("アラートサービスを初期化します。");
			InitializeAlert();
			
			// コマンドライン引数解析
			logger_.Information("コマンドライン引数を解析します。");
			logger_.VerboseFormat("スタートアップ引数:{0}", new object[] { string.Join(",", e.Args) });

			var options = new StartupOptions();
			var parser = new Parser(settings => {
				settings.CaseSensitive = false;
				settings.ParsingCulture = System.Globalization.CultureInfo.CurrentCulture;
			});

			if (!parser.ParseArguments(e.Args, options)) {
				logger_.Error("不正なコマンドラインオプションです。");
				alertManager_.ShowErrorMessage("不正なコマンドラインオプションです。");
				Shutdown();
				return;
			}

			logger_.Information("コマンドライン引数の解析を完了しました。");


			// アプリケーションインスタンスを作成
			if (options.UpdateMode) {
				application_ = new UpdaterApplication();
			}
			else {
				application_ = new HanburaAplication();
			}

			// アプリケーションを初期化
			application_.OnStartup(this, options, alertManager_, logger_);
			logger_.Information("アプリケーションの初期化が完了しました。");
		}

		protected override void OnExit(ExitEventArgs e)
		{
			logger_.InformationFormat("アプリケーションを終了します。(Code:{0})", new object[]{e.ApplicationExitCode});
			if (application_ != null) {
				application_.OnShutdown(this, logger_);
				application_ = null;
			}
			logger_.Information("アプリケーションの終了処理完了");

			disposables_.Dispose();
			disposables_ = null;

			base.OnExit(e);
		}


		/// <summary>
		/// キャッチされなかった例外を処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			if (logger_ != null) {
				logger_.Fatal("予期しないエラーが発生しました。",  e.Exception);
			}

			var msg = "予期せぬエラーが発生したため、アプリケーションを終了します。";
			if (alertManager_ != null) {
				alertManager_.ShowErrorMessage(msg, e.Exception);
			}
			else {
				MessageBox.Show(
					string.Format("{0}\n\n【例外情報】\n{1}", msg, e.Exception),
					"エラー",
					MessageBoxButton.OK, MessageBoxImage.Stop);
			}

			e.Handled = true;
			Shutdown(-1);
		}

	}
}
