using System;
using System.Globalization;
using System.Windows.Data;

namespace supermarket.Utils.Convertes
{
    [ValueConversion(typeof(string), typeof(string))]
    public class DateStringToShortConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = DateTime.Parse((string)value);
            return date.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}