using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AffirmationBar.WPF.Converters
{
	class SemesterTypeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Semester valueCame = (Semester) value;

			switch (valueCame.Type)
			{
				case SemesterType.Summer:
					return "Летен";
				case SemesterType.Winter:
					return "Зимен";
				default:
					return "Няма информация";
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
