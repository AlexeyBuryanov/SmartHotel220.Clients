using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// Конвертер значения времени регистрации в номере
    /// </summary>
    public class CheckInTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var time = value.ToString();
                var date = DateTime.Parse(time, culture);

                return date.ToString("HH:mm tt");
            }
            catch
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
