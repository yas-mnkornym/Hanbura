using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Studiotaiha.Hanbura.Views.Components
{
	/// <summary>
	/// SystemButtons.xaml の相互作用ロジック
	/// </summary>
	public partial class SystemButtons : UserControl
	{
		public SystemButtons()
		{
			InitializeComponent();
		}

		public bool CanMinimize
		{
			get { return (bool)GetValue(CanMinimizeProperty); }
			set { SetValue(CanMinimizeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for CanMinimize.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CanMinimizeProperty =
			DependencyProperty.Register("CanMinimize", typeof(bool), typeof(SystemButtons), new PropertyMetadata(true));

		public bool CanMaximize
		{
			get { return (bool)GetValue(CanMaximizeProperty); }
			set { SetValue(CanMaximizeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for CanMaximize.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CanMaximizeProperty =
			DependencyProperty.Register("CanMaximize", typeof(bool), typeof(SystemButtons), new PropertyMetadata(true));


		void CanMinimizeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var isVisible = (bool)e.NewValue;
			button_Minimize.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
		}

		void CanMaximizeChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var isVisible = (bool)e.NewValue;
			button_Maximize.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
		}

		private void button_Close_Click(object sender, RoutedEventArgs e)
		{
			var window = Window.GetWindow(this);
			if (window != null) {
				window.Close();
			}
		}

		private void button_Restore_Click(object sender, RoutedEventArgs e)
		{
			var window = Window.GetWindow(this);
			if (window != null) {
				window.WindowState = WindowState.Normal;
			}
		}

		private void button_Maximize_Click(object sender, RoutedEventArgs e)
		{
			var window = Window.GetWindow(this);
			if (window != null) {
				window.WindowState = WindowState.Maximized;
			}
		}

		private void button_Minimize_Click(object sender, RoutedEventArgs e)
		{
			var window = Window.GetWindow(this);
			if (window != null) {
				window.WindowState = WindowState.Minimized;
			}
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			var window = Window.GetWindow(this);
			if (window != null) {
				window.StateChanged += window_StateChanged;
				UpdateButtons();
			}
		}

		void window_StateChanged(object sender, EventArgs e)
		{
			UpdateButtons();
		}

		void UpdateButtons()
		{
			var window = Window.GetWindow(this);
			if (window == null) { return; }

			switch (window.WindowState) {
				case WindowState.Normal:
					button_Maximize.Visibility = CanMaximize ? Visibility.Visible : Visibility.Collapsed;
					button_Minimize.Visibility = CanMinimize ? Visibility.Visible : Visibility.Collapsed;
					button_Restore.Visibility = Visibility.Collapsed;
					break;

				case WindowState.Maximized:
					button_Maximize.Visibility = Visibility.Collapsed;
					button_Minimize.Visibility = CanMinimize ? Visibility.Visible : Visibility.Collapsed;
					button_Restore.Visibility = Visibility.Visible;
					break;
			}
		}
	}
}
