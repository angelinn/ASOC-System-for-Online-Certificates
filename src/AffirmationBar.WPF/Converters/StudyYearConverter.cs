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
	class StudyYearConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			StudyYear year = (StudyYear) value;
			switch (year)
			{
				case StudyYear.First:
					return "Първа";
				case StudyYear.Second:
					return "Втора";
				case StudyYear.Third:
					return "Трета";
				case StudyYear.Fourth:
					return "Четвърта";
				case StudyYear.NoStudentAccess:
					return "Няма студентски права";
				default:
					return "Невалиден номер";
			}
			}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
