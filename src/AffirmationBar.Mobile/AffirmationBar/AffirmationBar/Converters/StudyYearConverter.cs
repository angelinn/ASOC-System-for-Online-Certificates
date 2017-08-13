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
    public class StudyYearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StudyYear year = (StudyYear)value;
            switch (year)
            {
                case StudyYear.First:
                    return "първа";
                case StudyYear.Second:
                    return "втора";
                case StudyYear.Third:
                    return "трета";
                case StudyYear.Fourth:
                    return "четвърта";
                default:
                    return "Ексепшън...";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
