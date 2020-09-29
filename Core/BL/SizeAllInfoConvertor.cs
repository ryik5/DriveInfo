using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Core.BL
{
    public class SizeAllInfoConvertor : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";
            double val;
            foreach (var data in value)
            {
                if (data != null)
                {
                    try { val = (double)data; } catch { val = 0; }


                    switch (val)
                    {
                        case 0:
                            if (result.Length > 0)
                                result += "0 GB";
                            else
                                result += "0 GB/";
                            break;
                        default:
                            if (result.Length > 0)
                                result += "/" + (val / 1024 / 1024 / 1024).ToString("0.00 GB");
                            else
                                result += (val / 1024 / 1024 / 1024).ToString("0.00 GB");
                            break;
                    }
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
