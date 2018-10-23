using System;
using System.Globalization;

namespace SmartHotel220.Clients.Core.Models
{
    public interface ILocationResponse
    {
    }

    /// <summary>
    /// Геолокация
    /// </summary>
    public class GeoLocation : ILocationResponse
    {
        /// <summary>
        /// Широта
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double Longitude { get; set; }

        public static GeoLocation Parse(string location)
        {
            var result = new GeoLocation();

            try
            {
                const string locationSetting = AppSettings.DefaultFallbackMapsLocation;
                var locationParts = locationSetting.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                result.Latitude = double.Parse(locationParts[0], CultureInfo.InvariantCulture);
                result.Longitude = double.Parse(locationParts[1], CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка парсинга местоположения: {ex}");
            }

            return result;
        }
    }
}
