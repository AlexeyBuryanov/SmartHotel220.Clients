using SmartHotel220.Clients.Core.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// Тип предложения к цвету
    /// </summary>
    public class SuggestionTypeToColorConverter : IValueConverter
    {
        private readonly Color _restaurantColor = Color.FromHex("#BD4B14");
        private readonly Color _eventColor = Color.FromHex("#348E94");
        private readonly Color _noColor = Color.FromHex("#FFFFFF");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SuggestionType suggestionType)
            {
                switch(suggestionType)
                {
                    case SuggestionType.Event:
                        return _eventColor;
                    case SuggestionType.Restaurant:
                        return _restaurantColor;
                }
            }

            return _noColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}