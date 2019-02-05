using Windows.UI;

namespace SmartHotel220.Clients.UWP.Extensions
{
    /// <summary>
    /// Расширение для цвета
    /// </summary>
    internal static class ColorExtensions
    {
        /// <summary>
        /// Для рендера текст-бокса
        /// </summary>
        public static Color ToUwp(this Xamarin.Forms.Color color)
        {
            return Color.FromArgb((byte)(color.A * 255),
                                  (byte)(color.R * 255),
                                  (byte)(color.G * 255),
                                  (byte)(color.B * 255));
        }
    }
}
