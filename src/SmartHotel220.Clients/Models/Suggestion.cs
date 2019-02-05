namespace SmartHotel220.Clients.Core.Models
{
    /// <summary>
    /// Тип предложения
    /// </summary>
    public enum SuggestionType
    {
        Event,
        Restaurant
    }

    /// <summary>
    /// Предложение
    /// </summary>
    public class Suggestion
    {
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Картинка
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Рейтинг
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Кол-во голосов
        /// </summary>
        public int Votes { get; set; }

        /// <summary>
        /// Тип предложения
        /// </summary>
        public SuggestionType SuggestionType { get; set; }
    }
}