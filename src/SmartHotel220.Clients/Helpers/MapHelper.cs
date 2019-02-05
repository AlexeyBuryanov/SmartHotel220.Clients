using SmartHotel220.Clients.Core.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms.Maps;

namespace SmartHotel220.Clients.Core.Helpers
{
    /// <summary>
    /// Помошник для работы с картой
    /// </summary>
    public static class MapHelper
    {
        /// <summary>
        /// Посчитать дистанцию
        /// </summary>
        /// <param name="lat1">широта1</param>
        /// <param name="lon1">долгота1</param>
        /// <param name="lat2">широта2</param>
        /// <param name="lon2">долгота2</param>
        /// <param name="unit">ед. измерения</param>
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            var theta = lon1 - lon2;
            var dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) + Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));
            dist = Math.Acos(dist);
            dist = Rad2Deg(dist);
            dist = dist * 60 * 1.1515;

            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }

            return (dist);
        }

        /// <summary>
        /// Центрировать карту на локации по дефолту
        /// </summary>
        /// <param name="map">карта</param>
        internal static void CenterMapInDefaultLocation(Map map)
        {
            try
            {
                var location = GeoLocation.Parse(AppSettings.FallbackMapsLocation);
                var initialPosition = new Position(location.Latitude, location.Longitude);

                var mapSpan = MapSpan.FromCenterAndRadius(initialPosition, Distance.FromKilometers(1.6));

                map.MoveToRegion(mapSpan);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"[MapHelper] Ошибка: {ex}");
            }
        }

        private static double Deg2Rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double Rad2Deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}