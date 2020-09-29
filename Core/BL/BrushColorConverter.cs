using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Core.Models;

namespace Core.BL
{
    public class BrushColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color result = Colors.Black;

            if (value is null)
                return new SolidColorBrush(result);

            DiskType diskType = DiskType.Unknown;
            try { diskType = (DiskType)value; } catch { }

            switch (diskType)
            {
                case DiskType.Removable:
                    result = Colors.PaleGreen;
                    break;
                case DiskType.Fixed:
                    result = Colors.Black;
                    break;
                default:
                    result = Colors.PaleVioletRed;
                    break;
            }

            return new SolidColorBrush(result);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
