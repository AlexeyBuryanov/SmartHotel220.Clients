using SmartHotel220.Clients.Core.Extensions;
using SmartHotel220.Clients.Core.Services.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Booking
{
    /// <summary>
    /// Служба бронирования
    /// </summary>
    public class BookingService : IBookingService
    {
        /// <summary>
        /// Служба запросов
        /// </summary>
        private readonly IRequestService _requestService;

        public BookingService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        /// <summary>
        /// Получить бронирования
        /// </summary>
        public Task<IEnumerable<Models.BookingSummary>> GetBookingsAsync(string token = "")
        {
            var builder = new UriBuilder(AppSettings.BookingEndpoint);
            builder.AppendToPath("Bookings");

            var uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.BookingSummary>>(uri, token);
        }

        /// <summary>
        /// Получить последние бронирования
        /// </summary>
        public Task<IEnumerable<Models.BookingSummary>> GetLatestBookingsAsync(string token = "")
        {
            var builder = new UriBuilder(AppSettings.BookingEndpoint);
            builder.AppendToPath("Bookings/latest");

            var uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.BookingSummary>>(uri, token);
        }

        /// <summary>
        /// Получить бронирования по email
        /// </summary>
        public Task<IEnumerable<Models.BookingSummary>> GetBookingsByEmailAsync(string userId, string token = "")
        {
            if (!string.IsNullOrEmpty(token))
            {
                var builder = new UriBuilder(AppSettings.BookingEndpoint);
                builder.AppendToPath($"Bookings/{userId}");

                var uri = builder.ToString();

                return _requestService.GetAsync<IEnumerable<Models.BookingSummary>>(uri, token);
            }
            
            return Task.FromResult<IEnumerable<Models.BookingSummary>>(new List<Models.BookingSummary>());
        }

        /// <summary>
        /// Получить последние бронирования по email
        /// </summary>
        public Task<IEnumerable<Models.BookingSummary>> GetLatestBookingsByEmailAsync(string email, string token = "")
        {
            var builder = new UriBuilder(AppSettings.BookingEndpoint);
            builder.AppendToPath($"Bookings/latest/{email}");

            var uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.BookingSummary>>(uri, token);
        }

        /// <summary>
        /// Создать бронирование
        /// </summary>
        public Task<Models.Booking> CreateBookingAsync(Models.Booking booking, string token = "")
        {
            var builder = new UriBuilder(AppSettings.BookingEndpoint);
            builder.AppendToPath("Bookings");

            var uri = builder.ToString();

            return _requestService.PostAsync(uri, booking, token);
        }

        /// <summary>
        /// Получить информацию о заполняемости номера по дате
        /// </summary>
        public Task<Models.Occupancy> GetOccupancyInformationAsync(int roomId, DateTime date)
        {
            var builder = new UriBuilder(AppSettings.BookingEndpoint);
            builder.AppendToPath($"Rooms/{roomId}/occupancy");
            builder.Query = $"date={date.ToString("MM/dd/yyyy")}";

            var uri = builder.ToString();

            return _requestService.GetAsync<Models.Occupancy>(uri);
        }
    }
}