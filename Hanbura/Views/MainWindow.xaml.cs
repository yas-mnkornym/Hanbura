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
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			AdjustWindowByWebBrowserSize(webBrowser_.Width, webBrowser_.Height);
		}

		void AdjustWindowByWebBrowserSize(double width, double height)
		{
			var controls = new FrameworkElement[]{ webBrowser_, workingBackground_};

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

			// ウィンドウにあわせてコントロールを変形
			Width = ActualWidth;
			Height = ActualHeight;
			SizeToContent = System.Windows.SizeToContent.Manual;
			foreach (var control in controls) {
				control.Width = double.NaN;
				control.Height = double.NaN;
				control.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
				control.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
			}
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
	}
}
