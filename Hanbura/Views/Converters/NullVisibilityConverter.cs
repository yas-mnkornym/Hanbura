using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Studiotaiha.Hanbura.Views.Converters
{
	public sealed class NullVisibilityConverter : IValueConverter
	{
		public NullVisibilityConverter()
		{
			NullVisiblity = Visibility.Collapsed;
			NotNullVisibility = Visibility.Visible;
		}

		/// <summary>
		/// NullのときのVisibilityを設定・取得する
		/// </summary>
		/// <remarks>default: Collapsed</remarks>
		public Visibility NullVisiblity { get; set; }

		/// <summary>
		/// NullでないときのVisibilityを設定・取得する
		/// </summary>
		/// <remarks>default: Visible</remarks>
		public Visibility NotNullVisibility { get; set; }


		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (value == null ? NullVisiblity : NotNullVisibility);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
