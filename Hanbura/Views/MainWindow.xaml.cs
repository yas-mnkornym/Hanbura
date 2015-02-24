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
using System.Windows.Shapes;

namespace Studiotaiha.Hanbura.Views
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{


		public double ContentWidth
		{
			get { return (double)GetValue(ContentWidthProperty); }
			set { SetValue(ContentWidthProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ContentWidth.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ContentWidthProperty =
			DependencyProperty.Register("ContentWidth", typeof(double), typeof(MainWindow), new PropertyMetadata(800.0));



		public double ContentHeight
		{
			get { return (double)GetValue(ContentHeightProperty); }
			set { SetValue(ContentHeightProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ContentHeight.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ContentHeightProperty =
			DependencyProperty.Register("ContentHeight", typeof(double), typeof(MainWindow), new PropertyMetadata(480.0));


		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			AdjustWindowByWebBrowserSize(ContentWidth, ContentHeight);
		}

		void AdjustWindowByWebBrowserSize(double width, double height)
		{
			var controls = new FrameworkElement[]{ webBrowser_, workingBackground_};

			var oldWindowStyle = WindowStyle;
			WindowStyle = System.Windows.WindowStyle.None;
			var oldResizeMode = ResizeMode;
			ResizeMode = System.Windows.ResizeMode.NoResize;

			// コントロールに合わせてウィンドウを変形
			foreach (var control in controls) {
				control.Width = width;
				control.Height = height;
				control.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
				control.VerticalAlignment = System.Windows.VerticalAlignment.Top;
			}
			Width = double.NaN;
			Height = double.NaN;
			SizeToContent = System.Windows.SizeToContent.WidthAndHeight;

			Dispatcher.BeginInvoke((Action)(() => {
				// ウィンドウにあわせてコントロールを変形
				SizeToContent = System.Windows.SizeToContent.Manual;
				Width = ActualWidth;
				Height = ActualHeight;
				foreach (var control in controls) {
					control.Width = double.NaN;
					control.Height = double.NaN;
					control.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
					control.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
				}

				WindowStyle = oldWindowStyle;
				ResizeMode = oldResizeMode;
			}), System.Windows.Threading.DispatcherPriority.Loaded);
		}

		private void Window_StateChanged(object sender, EventArgs e)
		{
			// マージンを調整
			if (WindowState == System.Windows.WindowState.Maximized) {
				// 最大化時この分だけ削られてしまうのでその対策
				var systemDefault = SystemParameters.WindowResizeBorderThickness;
				panelMain_.Margin = new Thickness(systemDefault.Left * 2, systemDefault.Top * 2, systemDefault.Right * 2, systemDefault.Bottom * 2);
				webBrowserWrap_.Padding = new Thickness(0.0);
			}
			else {
				panelMain_.Margin = new Thickness(0.0);
				webBrowserWrap_.Padding = new Thickness(2.0, 0.0, 2.0, 2.0);
			}
		}

		/// <summary>
		/// 表示倍率変更ボタンクリック時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_ChangeScale_Click(object sender, RoutedEventArgs e)
		{
			try {
				var button = sender as Button;
				if (button != null && button.Tag != null) {
					double scale = (double)button.Tag;
						var width = ContentWidth * scale;
						var height = ContentHeight * scale;
						AdjustWindowByWebBrowserSize(width, height);
				}
			}
			catch (Exception ex) {
				AlertService.Current.AlertManager.ShowErrorMessage("表示倍率の変更に失敗しました。", ex);
			}

			dropDownButton_Scale.IsOpen = false;
		}
	}
}
