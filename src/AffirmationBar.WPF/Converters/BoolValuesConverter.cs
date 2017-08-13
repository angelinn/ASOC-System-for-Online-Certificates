using System;
using System.Globalization;
using System.Windows.Data;

namespace AffirmationBar.WPF.Converters
{
	class BoolValuesConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool valueCame = (bool)value;
			return !valueCame;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
