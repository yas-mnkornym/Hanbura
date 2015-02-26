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
	}
}
