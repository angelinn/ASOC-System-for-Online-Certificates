using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AffirmationBar.Converters
{
    public class SemesterTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SemesterType semesterType = (SemesterType)value;
            switch (semesterType)
            {
                case SemesterType.Summer:
                    return "летен";
                case SemesterType.Winter:
                    return "зимен";
                default:
                    return "ексепшън";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
