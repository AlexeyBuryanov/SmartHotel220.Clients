using SmartHotel220.Clients.Core.Controls;
using SmartHotel220.Clients.Core.Helpers;
using SmartHotel220.Clients.Core.Models;
using SmartHotel220.Clients.UWP.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace SmartHotel220.Clients.UWP.Renderers
{
    /// <summary>
    /// Рендерер карты
    /// </summary>
    public class CustomMapRenderer : MapRenderer
    {
        // Ресурс события
        private readonly RandomAccessStreamReference _eventResource = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pushpin_01.png"));
        // Ресурс ресторана
        private readonly RandomAccessStreamReference _restaurantResource = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pushpin_02.png"));

        private CustomMap _customMap;
        private List<CustomPin> _customPins;
        private List<CustomMapIcon> _tempMapIcons;

        public CustomMapRenderer()
        {
            _customPins = new List<CustomPin>();
            _tempMapIcons = new List<CustomMapIcon>();
        }

        /// <summary>
        /// При изменении св-ва привязки
        /// </summary>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var windowsMapView = Control;
            _customMap = (CustomMap)sender;

            // Если это CustomPins
            if (e.PropertyName.Equals("CustomPins"))
            {
                // Проводим очистку затем добавление
                _customPins = _customMap.CustomPins.ToList();
                ClearPushPins(windowsMapView);

                AddPushPins(windowsMapView, _customMap.CustomPins);
                PositionMap();
            }
        }

        /// <summary>
        /// Очистка элементов карты
        /// </summary>
        private void ClearPushPins(MapControl mapControl)
        {
            mapControl.MapElements.Clear();
        }

        /// <summary>
        /// Добавить пины
        /// </summary>
        private void AddPushPins(MapControl mapControl, IEnumerable<CustomPin> pins)
        {
            _tempMapIcons?.Clear();

            foreach (var pin in pins)
            {
                // Основная информация для описания географического положения
                var snPosition = new BasicGeoposition { Latitude = pin.Position.Latitude, Longitude = pin.Position.Longitude };
                // Географическая точка на основе позиции
                var snPoint = new Geopoint(snPosition);

                var mapIcon = new CustomMapIcon
                {
                    Label = pin.Label,
                    MapIcon = new MapIcon
                    {
                        MapTabIndex = pin.Id,
                        CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible,
                        Location = snPoint,
                        NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0),
                        ZIndex = 0
                    }
                };

                // Подбираем иконку в зависимости от заведения
                switch (pin.Type)
                {
                    case SuggestionType.Event:
                        mapIcon.MapIcon.Image = _eventResource;
                        break;
                    case SuggestionType.Restaurant:
                        mapIcon.MapIcon.Image = _restaurantResource;
                        break;
                    default:
                        mapIcon.MapIcon.Image = _eventResource;
                        break;
                }

                mapControl.MapElements.Add(mapIcon.MapIcon);
                _tempMapIcons?.Add(mapIcon);
            }
        }

        /// <summary>
        /// Позиционирование карты
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

    public class CustomMapIcon
    {
        public string Label { get; set; }
        public MapIcon MapIcon { get; set; }
    }
}