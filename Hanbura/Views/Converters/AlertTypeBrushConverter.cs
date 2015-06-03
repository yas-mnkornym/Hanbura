using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Studiotaiha.Hanbura.Views.Converters
{
	internal class AlertTypeBrushConverter : IValueConverter
	{
		public AlertTypeBrushConverter()
		{
			DefaultBrush = (Brush)App.Current.Resources["DefaultAlertBrushKey"];
			DebugBrush = (Brush)App.Current.Resources["DebugAlertBrushKey"];
			InformationBrush = (Brush)App.Current.Resources["InformationAlertBrushKey"];
			ExclamationBrush = (Brush)App.Current.Resources["ExclamationAlertBrushKey"];
			WarningBrush = (Brush)App.Current.Resources["WarningAlertBrushKey"];
			ErrorBrush = (Brush)App.Current.Resources["ErrorAlertBrushKey"];
			QuestionBrush = (Brush)App.Current.Resources["QuestionAlertBrushKey"];
		}

		public Brush DefaultBrush { get; set; }

		public Brush DebugBrush { get; set; }

		public Brush InformationBrush { get; set; }

		public Brush ExclamationBrush { get; set; }

		public Brush WarningBrush { get; set; }

		public Brush ErrorBrush { get; set; }

		public Brush QuestionBrush { get; set; }

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var alertType = (EAlertType)value;
			switch (alertType) {
				case EAlertType.Debug:
					return DebugBrush;

				case EAlertType.Information:
					return InformationBrush;

				case EAlertType.Exclamation:
					return ExclamationBrush;

				case EAlertType.Warning:
					return WarningBrush;

				case EAlertType.Error:
					return ErrorBrush;

				case EAlertType.Question:
					return QuestionBrush;

				default:
					return DefaultBrush;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
