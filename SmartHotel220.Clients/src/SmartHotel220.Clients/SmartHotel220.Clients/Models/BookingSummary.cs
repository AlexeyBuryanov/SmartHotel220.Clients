using System;

namespace SmartHotel220.Clients.Core.Models
{
    /// <summary>
    /// Бронирование, общее
    /// </summary>
    public class BookingSummary
    {
        public int Id { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Отель
        /// </summary>
        public int HotelId { get; set; }

        /// <summary>
        /// От
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// До
        /// </summary>
        public DateTime To { get; set; }
    }
}