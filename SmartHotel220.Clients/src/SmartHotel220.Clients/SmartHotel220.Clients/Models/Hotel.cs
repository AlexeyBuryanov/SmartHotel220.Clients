using System.Collections.Generic;

namespace SmartHotel220.Clients.Core.Models
{
    /// <summary>
    /// Отель
    /// </summary>
    public class Hotel
    {
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public int CityId { get; set; }
        public string City { get; set; }

        /// <summary>
        /// Рейтинг
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Цена за ночь
        /// </summary>
        public int PricePerNight { get; set; }

        /// <summary>
        /// Картинка
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Картинка по умолчанию
        /// </summary>
        public string DefaultPicture { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Время регистрации
        /// </summary>
        public string CheckInTime { get; set; }

        /// <summary>
        /// Время выписки
        /// </summary>
        public string CheckOutTime { get; set; }

        /// <summary>
        /// Картинки
        /// </summary>
        public List<string> Pictures { get; set; }

        /// <summary>
        /// Номера
        /// </summary>
        public List<Room> Rooms { get; set; }

        /// <summary>
        /// Сервисы/службы
        /// </summary>
        public List<Service> Services { get; set; }
    }
}