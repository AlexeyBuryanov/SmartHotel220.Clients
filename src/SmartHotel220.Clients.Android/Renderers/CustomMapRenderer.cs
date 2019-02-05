using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using SmartHotel220.Clients.Core.Controls;
using SmartHotel220.Clients.Core.Helpers;
using SmartHotel220.Clients.Core.Models;
using SmartHotel220.Clients.Droid.Renderers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace SmartHotel220.Clients.Droid.Renderers
{
    /// <summary>
    /// Рендер карты
    /// </summary>
    public class CustomMapRenderer : MapRenderer
    {
        // Ресурс события
        private const int EventResource = Resource.Drawable.pushpin_01;
        // Ресурс ресторана
        private const int RestaurantResource = Resource.Drawable.pushpin_02;

        // Иконка пина
        private BitmapDescriptor _pinIcon;
        // Временные маркеры
        private readonly List<CustomMarkerOptions> _tempMarkers;
        // Карта отрисовалась нормально или нет
        private bool _isDrawnDone;

        public CustomMapRenderer()
        {
            _tempMarkers = new List<CustomMarkerOptions>();
            _pinIcon = BitmapDescriptorFactory.FromResource(EventResource);
        }

        /// <summary>
        /// При изменении св-ва привязки
        /// </summary>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            // Получаем карту андроида
            var androidMapView = Control;
            // Получаем карту формс
            var formsMap = (CustomMap)sender;

            // Если св-во равно кастомным пинам и при этом карта ещё не отрисовалась
            if (e.PropertyName.Equals("CustomPins") && !_isDrawnDone)
            {
                // Очищаем пины
                ClearPushPins(androidMapView);

                // Доступна ли локация 
                NativeMap.MyLocationEnabled = formsMap.IsShowingUser;

                // Добавляем наши кастомные пины
                AddPushPins(androidMapView, formsMap.CustomPins);

                // Позиционируем карту
                PositionMap();

                // Отрисовано хорошо
                _isDrawnDone = true;
            }
        } 

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (changed)
            {
                _isDrawnDone = false;
            }
        }

        private void ClearPushPins(MapView mapView)
        {
            NativeMap.Clear();
        }

        /// <summary>
        /// Добавить пины
        /// </summary>
        /// <param name="mapView">Карта</param>
        /// <param name="pins">Пины</param>
        private void AddPushPins(MapView mapView, IEnumerable<CustomPin> pins)
        {
            foreach (var formsPin in pins)
            {
                // Настраиваем маркер
                var markerWithIcon = new MarkerOptions();

                markerWithIcon.SetPosition(new LatLng(formsPin.Position.Latitude, formsPin.Position.Longitude));
                markerWithIcon.SetTitle(formsPin.Label);
                markerWithIcon.SetSnippet(formsPin.Address);

                // Подбираем иконку в зависимости от заведения
                switch (formsPin.Type)
                {
                    // Событие
                    case SuggestionType.Event:
                        _pinIcon = BitmapDescriptorFactory.FromResource(EventResource);
                        markerWithIcon.SetIcon(_pinIcon);
                        break;

                    // Ресторан
                    case SuggestionType.Restaurant:
                        _pinIcon = BitmapDescriptorFactory.FromResource(RestaurantResource);
                        markerWithIcon.SetIcon(_pinIcon);
                        break;

                    // По дефолту
                    default:
                        markerWithIcon.SetIcon(BitmapDescriptorFactory.DefaultMarker());
                        break;
                }

                // Добавляем новую опцию маркера
                NativeMap.AddMarker(markerWithIcon);

                _tempMarkers.Add(new CustomMarkerOptions
                {
                    Id = formsPin.Id,
                    MarkerOptions = markerWithIcon
                });
            }
        }

        /// <summary>
        /// Позиционировать карту
        /// </summary>
        private void PositionMap()
        {
            // Наша карта
            var myMap = Element as CustomMap;
            // Наши пины
            var formsPins = myMap?.CustomPins;

            if (formsPins == null || formsPins.Count() == 0)
            {
                return;
            }

            // Центральная позиция на основе средних координат
            var centerPosition = new Position(formsPins.Average(x => x.Position.Latitude), formsPins.Average(x => x.Position.Longitude));

            // Минимальные координаты
            var minLongitude = formsPins.Min(x => x.Position.Longitude);
            var minLatitude = formsPins.Min(x => x.Position.Latitude);

            // Максимальные координаты
            var maxLongitude = formsPins.Max(x => x.Position.Longitude);
            var maxLatitude = formsPins.Max(x => x.Position.Latitude);

            // Дистанция в километрах
            var distance = MapHelper.CalculateDistance(minLatitude, minLongitude, maxLatitude, maxLongitude, 'K');

            // Перемещаемся в регион по центральной позиции в радиусе доступных координат
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(distance)));
        }
    }

    /// <summary>
    /// Для хранения MarkerOptions
    /// </summary>
    public class CustomMarkerOptions
    {
        public int Id { get; set; }
        public MarkerOptions MarkerOptions { get; set; }
    }
}