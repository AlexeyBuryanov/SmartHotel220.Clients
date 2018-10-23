using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// Конвертер значения окружающего света
    /// </summary>
    public class AmbientLightValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double))
            {
                throw new InvalidOperationException("Значение должно быть типа double");
            }

            var lightValue = (double)value;

            return $"{lightValue.ToString("N0")}K";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
