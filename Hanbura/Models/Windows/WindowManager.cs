using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Hanbura.Windows;

namespace Studiotaiha.Hanbura.Models.Windows
{
	internal class WindowManager : Dispatchable, IWindowManager, IDisposable
	{
		Settings settings_;
		LinkedList<ChildWindow> windows_ = new LinkedList<ChildWindow>();

		#region Logger
		#endregion


		public WindowManager(
			Settings settings,
			IDispatcher dispatcher)
			: base(dispatcher)
		{
			if (settings == null) { throw new ArgumentNullException("settings"); }
			settings_ = settings;
		}

		public IWindow CreateWindow(string tag, WindowConfig config, IWindow owner = null)
		{
			if (tag == null) { throw new ArgumentNullException("tag"); }
			if (config == null) { throw new ArgumentNullException("config"); }

			if (windows_.Any(x => x.Tag == tag)) { throw new WindowTagAlreadyExistsException(tag); }
			var windowSettings = settings_.GetWindowSettings(string.Format("__ChildWindow_{0}", tag));
			var window = new ChildWindow(config, windowSettings, Dispatcher, wnd => {
				var ownerWindow = owner as ChildWindow;
				if (ownerWindow != null) {
					wnd.SetOwner(ownerWindow);
				}
				else {
					wnd.SetOwner(App.Current.MainWindow);
				}
			});
			windows_.AddLast(window);

			window.Closed += window_Closed;

			return window;
		}

		/// <summary>
		/// ウィンドウが閉じられたら登録解除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void window_Closed(object sender, EventArgs e)
		{
			var childWindow = sender as ChildWindow;
			if (childWindow == null) { return; }

			windows_.Remove(childWindow);
			childWindow.Closed -= window_Closed;
		}

		public IWindow FindWindow(string tag)
		{
			return Windows.FirstOrDefault(x => x.Tag == tag);
		}

		public IEnumerable<IWindow> Windows
		{
			get
			{
				return windows_;
			}
		}

		public IEnumerable<string> Tags
		{
			get
			{
				return windows_.Select(x => x.Tag);
			}
		}

		public IDispatcher UIThreadDispatcher
		{
			get
			{
				return Dispatcher;
			}
		}


		#region IDisposable メンバ
		bool isDisposed_ = false;
		virtual protected void Dispose(bool disposing)
		{
			if (isDisposed_) { return; }
			if (disposing) {
				foreach (var window in windows_) {
					try {
						window.Close();
					}
					catch (Exception ex) {
						Logger.Error("WindowのCloseに失敗しました。", ex);
					}
				}
				windows_.Clear();
			}
			isDisposed_ = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

		#region Logger
		ILogger logger_;
		ILogger Logger
		{
			get
			{
				return logger_ ?? (logger_ = LoggingService.Current.GetLogger(this));
			}
		}
		#endregion
	}
}
