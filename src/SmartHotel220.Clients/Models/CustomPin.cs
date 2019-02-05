using Xamarin.Forms.Maps;

namespace SmartHotel220.Clients.Core.Models
{
    /// <summary>
    /// Кастомный пин (для карты)
    /// </summary>
    public class CustomPin
    {
        public int Id { get; set; }

        /// <summary>
        /// Метка
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Иконка
        /// </summary>
        public string PinIcon { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Позиция (широта, долгота)
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Тип предложений
        /// </summary>
        public SuggestionType Type { get; set; }
    }
}