using SmartHotel220.Clients.Core.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SmartHotel220.Clients.Core.Controls
{
    /// <inheritdoc />
    /// <summary>
    /// Кастомная карта
    /// </summary>
    public class CustomMap : Map
    {
        /// <summary>
        /// Св-во привязки для кастомных пинов
        /// </summary>
        public static readonly BindableProperty CustomPinsProperty =
            BindableProperty.Create("CustomPins", typeof(IEnumerable<CustomPin>), typeof(CustomMap), 
                default(IEnumerable<CustomPin>), BindingMode.TwoWay);

        public IEnumerable<CustomPin> CustomPins
        {
            get => (IEnumerable<CustomPin>)GetValue(CustomPinsProperty);
            set => SetValue(CustomPinsProperty, value);
        }

        /// <summary>
        /// Св-во привязки для выбранных пинов
        /// </summary>
        public static readonly BindableProperty SelectedPinProperty =
            BindableProperty.Create("SelectedPin", typeof(CustomPin), typeof(CustomMap), null);

        /// <summary>
        /// Выбранный пин
        /// </summary>
        public CustomPin SelectedPin
        {
            get => (CustomPin)GetValue(SelectedPinProperty);
            set => SetValue(SelectedPinProperty, value);
        }
    }
}