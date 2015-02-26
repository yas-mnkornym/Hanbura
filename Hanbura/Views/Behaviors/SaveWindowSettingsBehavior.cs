using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using Studiotaiha.Hanbura.Models;

namespace Studiotaiha.Hanbura.Views.Behaviors
{
	internal class SaveWindowSettingsBehavior : Behavior<Window>
	{
		double lastLeft_, lastTop_;
		bool isInitialized_ = false;

		public WindowSettings Settings
		{
			get { return (WindowSettings)GetValue(SettingsProperty); }
			set { SetValue(SettingsProperty, value); }
		}

		public static readonly DependencyProperty SettingsProperty =
			DependencyProperty.Register("Settings", typeof(WindowSettings), typeof(SaveWindowSettingsBehavior), new PropertyMetadata(null));


		protected override void OnAttached()
		{
			base.OnAttached();

			AssociatedObject.ContentRendered += AssociatedObject_ContentRendered;
			AssociatedObject.LocationChanged += AssociatedObject_LocationChanged;
			AssociatedObject.SizeChanged += AssociatedObject_SizeChanged;
			AssociatedObject.StateChanged += AssociatedObject_StateChanged;
		}

		void AssociatedObject_ContentRendered(object sender, EventArgs e)
		{
			var window = AssociatedObject;
			if (Settings != null) {
				if (Settings.RestorePosition) {
					if (!double.IsNaN(Settings.Left)) { window.Left = Settings.Left; }
					if (!double.IsNaN(Settings.Top)) { window.Top = Settings.Top; }
				}
				else {
					Settings.Left = window.Left;
					Settings.Top = window.Top;
				}

				if (Settings.RestoreSize) {
					if (!double.IsNaN(Settings.Width)) { window.Width = Settings.Width; }
					if (!double.IsNaN(Settings.Height)) { window.Height = Settings.Height; }
				}
				else {
					Settings.Width = window.ActualWidth;
					Settings.Height = window.ActualHeight;
				}

				if (Settings.RestorePosition || Settings.RestoreSize) {
					window.WindowState = Settings.State;
				}
			}

			isInitialized_ = true;
		}

		void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (!isInitialized_) { return; }
			if (Settings == null) { return; }

			if (AssociatedObject.WindowState == WindowState.Normal) {
				if (AssociatedObject.ActualWidth > 0) {
					Settings.Width = AssociatedObject.ActualWidth;
				}

				if (AssociatedObject.ActualHeight > 0) {
					Settings.Height = AssociatedObject.ActualHeight;
				}
			}
		}

		void AssociatedObject_StateChanged(object sender, EventArgs e)
		{
			if (!isInitialized_) { return; }
			if (Settings == null) { return; }

			var state = AssociatedObject.WindowState;
			if (state != WindowState.Minimized) {
				Settings.State = state;
			}

			if (state != WindowState.Normal) {
				Settings.Left = lastLeft_;
				Settings.Top = lastTop_;
			}
		}

		void AssociatedObject_LocationChanged(object sender, EventArgs e)
		{
			if (!isInitialized_) { return; }
			if (Settings == null) { return; }

			if (AssociatedObject.WindowState == WindowState.Normal) {
				lastLeft_ = Settings.Left;
				lastTop_ = Settings.Top;
				Settings.Left = AssociatedObject.Left;
				Settings.Top = AssociatedObject.Top;
			}
		}
	}
}
