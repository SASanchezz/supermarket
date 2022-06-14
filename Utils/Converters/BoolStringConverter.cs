using System;
using System.Globalization;
using System.Windows.Data;

namespace supermarket.Utils.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class BoolStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "True" ? "Так" : "Ні";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}