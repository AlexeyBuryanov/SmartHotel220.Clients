using SmartHotel220.Clients.Core.Extensions;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// Конвертер значения картинки отеля
    /// </summary>
    public class HotelImageConverter : IValueConverter
    {
        private readonly Random _rnd = new Random();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Если используются фейки
            if (AppSettings.UseFakes)
            {
                if (value != null)
                {
                    return value;
                }
                else
                {
                    var index = _rnd.Next(1, 9);

                    return Device.RuntimePlatform == Device.UWP 
                        ? string.Format("Assets/i_hotel_{0}.jpg", index) 
                        : string.Format("i_hotel_{0}", index);
                }
            }
            // Иначе строим URI к картинке
            else if (value != null)
            {
                var builder = new UriBuilder(AppSettings.ImagesBaseUri);
                builder.AppendToPath(value.ToString());

                return builder.ToString();
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}