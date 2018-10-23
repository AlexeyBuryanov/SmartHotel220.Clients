using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SmartHotel220.Clients.Core.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Location
{
    /// <inheritdoc />
    /// <summary>
    /// Сервис по работе с место нахождением
    /// </summary>
    public class LocationService : ILocationService
    {
        private readonly TimeSpan _positionReadTimeout = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Получить позицию
        /// </summary>
        public async Task<GeoLocation> GetPositionAsync()
        {
            try
            {
                // Кросс-платформенный геолокатор
                var locator = CrossGeolocator.Current;
                // Точность
                locator.DesiredAccuracy = 50;

                // Получаем позицию
                var position = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromMilliseconds((int)_positionReadTimeout.TotalMilliseconds));

                // Создаём нашу модель на основе полученных данных
                var geolocation = new GeoLocation
                {
                    Latitude = position.Latitude,
                    Longitude = position.Longitude,
                };

                return geolocation;
            }
            catch (GeolocationException geoEx)
            {
                Debug.WriteLine(geoEx);
            }
            catch (TaskCanceledException ex)
            {
                Debug.WriteLine(ex);
            }

            // По дефолту
            var defaultLocation = GeoLocation.Parse(AppSettings.DefaultFallbackMapsLocation);

            return defaultLocation;
        }
    }
}