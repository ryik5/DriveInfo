using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Core.BL
{
    public class SizeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double data = (double)value;
            string result;

            switch (data)
            {
                case 0:
                    result = "0";
                    break;
                default:
                    result = (data / 1000 / 1000 / 1000).ToString("0.00 GB");
                    break;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
