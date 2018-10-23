using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Booking
{
    /// <summary>
    /// Описывает службу бронирования
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Получить бронирования
        /// </summary>
        Task<IEnumerable<Models.BookingSummary>> GetBookingsAsync(string token = "");

        /// <summary>
        /// Получить последние бронирования
        /// </summary>
        Task<IEnumerable<Models.BookingSummary>> GetLatestBookingsAsync(string token = "");

        /// <summary>
        /// Получить бронирования по email
        /// </summary>
        Task<IEnumerable<Models.BookingSummary>> GetBookingsByEmailAsync(string email, string token = "");

        /// <summary>
        /// Получить последние бронирования по email
        /// </summary>
        Task<IEnumerable<Models.BookingSummary>> GetLatestBookingsByEmailAsync(string email, string token = "");

        /// <summary>
        /// Создать бронирование
        /// </summary>
        Task<Models.Booking> CreateBookingAsync(Models.Booking booking, string token = "");

        /// <summary>
        /// Получить информацию о заполняемости номера по дате
        /// </summary>
        Task<Models.Occupancy> GetOccupancyInformationAsync(int roomId, DateTime date);
    }
}