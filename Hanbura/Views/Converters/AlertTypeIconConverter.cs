using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Studiotaiha.Hanbura.Views.Converters
{
	class AlertTypeIconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			EAlertType alertType = (EAlertType)value;
			var icon = AlertTypeToIcon(alertType);

			if (icon == null) { return null; }
			else {
				return Imaging.CreateBitmapSourceFromHIcon(
					icon.Handle,
					Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
			}
		}

		Icon AlertTypeToIcon(EAlertType alertType)
		{
			System.Drawing.Icon icon = null;
			switch (alertType) {
				case EAlertType.Error:
					icon = System.Drawing.SystemIcons.Error;
					break;

				case EAlertType.Information:
				case EAlertType.Debug:
					icon = System.Drawing.SystemIcons.Information;
					break;

				case EAlertType.Question:
					icon = System.Drawing.SystemIcons.Question;
					break;

				case EAlertType.Warning:
				case EAlertType.Exclamation:
					icon = System.Drawing.SystemIcons.Exclamation;
					break;
			}
			return icon;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
