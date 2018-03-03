using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AffirmationBar.WPF.Converters
{
    public class VisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool valueCame = (bool)value;

			if (valueCame)
			{
				return Visibility.Visible;
			}

			return Visibility.Collapsed;


		}

	}
}
