using System;
using System.Collections.Generic;

namespace SmartHotel220.Clients.Core.Models
{
    /// <summary>
    /// Бронирование
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// Отель
        /// </summary>
        public int HotelId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// От
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// До
        /// </summary>
        public DateTime To { get; set; }

        /// <summary>
        /// Кол-во взрослых
        /// </summary>
        public int Adults { get; set; }

        /// <summary>
        /// Кол-во детей
        /// </summary>
        public int Kids { get; set; }

        /// <summary>
        /// Кол-во маленьких детей
        /// </summary>
        public int Babies { get; set; }

        /// <summary>
        /// Номера
        /// </summary>
        public List<Room> Rooms { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public int Price { get; set; }
    }
}