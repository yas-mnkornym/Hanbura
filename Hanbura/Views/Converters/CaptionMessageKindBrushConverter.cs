using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Studiotaiha.Hanbura.Views.Converters
{
	internal sealed class CaptionMessageKindBrushConverter : IValueConverter
	{
		public CaptionMessageKindBrushConverter()
		{
			DefaultBrush = Brushes.Transparent;
			InformationBrush = Brushes.DarkBlue;
			WarningBrush = Brushes.DarkRed;
		}

		public Brush DefaultBrush { get; set; }
		public Brush InformationBrush { get; set; }
		public Brush WarningBrush { get; set; }

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var kind = (CaptionMessageKind)value;
			switch (kind) {
				case CaptionMessageKind.Information:
					return InformationBrush;

				case CaptionMessageKind.Warning:
					return WarningBrush;

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
