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

namespace Studiotaiha.Hanbura.Views.Alerts
{
	/// <summary>
	/// StringAlertControl.xaml の相互作用ロジック
	/// </summary>
	public partial class StringAlertControl : UserControl
	{
		bool shouldReplace_ = false;

		public StringAlertControl()
		{
			InitializeComponent();
		}

		private void Expander_Expanded(object sender, RoutedEventArgs e)
		{
			shouldReplace_ = true;
		}

		private void Expander_Collapsed(object sender, RoutedEventArgs e)
		{
			shouldReplace_ = true;
		}

		private void Expander_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			// Expanderが開いたり閉じたりした時、ウィンドウの位置を調整する。
			if (shouldReplace_) {
				var window = Window.GetWindow(this);
				if (window == null) { return; }
				double centerX = window.Left + e.PreviousSize.Width / 2.0;
				double centerY = window.Top + e.PreviousSize.Height / 2.0;
				window.Left = centerX - (e.NewSize.Width / 2.0);
				window.Top = centerY - (e.NewSize.Height / 2.0);
				shouldReplace_ = false;
			}
		}
	}
}
