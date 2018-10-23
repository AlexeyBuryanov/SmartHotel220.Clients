using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// Количество к высоте. Используется для показа отзывов
    /// </summary>
    public class CountToHeightConverter : IValueConverter
    {
        private const int RowHeight = 300;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var count = System.Convert.ToInt32(value);

                return (RowHeight * count);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}