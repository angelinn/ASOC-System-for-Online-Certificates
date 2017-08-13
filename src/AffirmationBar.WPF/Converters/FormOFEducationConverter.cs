using SusiAPICommon.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace AffirmationBar.WPF.Converters
{
	class FormOFEducationConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			FormOfEducation valueCame = (FormOfEducation)value;
			switch (valueCame)
			{
				case FormOfEducation.Regular:
					return "Редовно";
				case FormOfEducation.Distance:
					return "Задочно";
				default:
					return "Не е намерена информация";
			}


		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
