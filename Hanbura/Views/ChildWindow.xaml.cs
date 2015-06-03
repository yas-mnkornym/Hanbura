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
	/// ChildWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class ChildWindow : Window
	{
		public ChildWindow()
		{
			InitializeComponent();
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (SizeToContent != System.Windows.SizeToContent.Manual) {
				var oldWindowStyle = WindowStyle;
				var oldResizeMode = ResizeMode;
				var oldSizeToContent = SizeToContent;
				SizeToContent = System.Windows.SizeToContent.Manual;

				Dispatcher.BeginInvoke((Action)(() => {
					SizeToContent = oldSizeToContent;
					ResizeMode = oldResizeMode;
					WindowStyle = oldWindowStyle;
				}), System.Windows.Threading.DispatcherPriority.Loaded);
			}
		}

		private void Window_StateChanged(object sender, EventArgs e)
		{
			// TODO: AllowsTransparency=trueの時、キャプションバーをダブルクリックで最大化するとおかしくなるの誰かなんとかして

			// マージンを調整
			if (WindowState == System.Windows.WindowState.Maximized) {
				// 最大化時この分だけ削られてしまうのでその対策
				var systemDefault = SystemParameters.WindowResizeBorderThickness;
				panelMain_.Margin = new Thickness(systemDefault.Left * 2, systemDefault.Top * 2, systemDefault.Right * 2, systemDefault.Bottom * 2);
				contentWrap_.BorderThickness = new Thickness(0.0);
				WindowState = System.Windows.WindowState.Maximized;
			}
			else {
				panelMain_.Margin = new Thickness(0.0);
				contentWrap_.BorderThickness = new Thickness(2.0, 0.0, 2.0, 2.0);
			}
		}
	}
}
