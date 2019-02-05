using SmartHotel220.Clients.Core.Extensions;
using SmartHotel220.Clients.Core.Models;
using SmartHotel220.Clients.Core.Services.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Hotel
{
    /// <inheritdoc />
    /// <summary>
    /// Служба по работе с отелями
    /// </summary>
    public class HotelService : IHotelService
    {
        /// <summary>
        /// Служба запросов
        /// </summary>
        private readonly IRequestService _requestService;

        public HotelService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        /// <summary>
        /// Получить города по дефолту
        /// </summary>
        public Task<IEnumerable<City>> GetCitiesAsync()
        {
            var builder = new UriBuilder(AppSettings.HotelsEndpoint);
            builder.AppendToPath("Cities");

            var uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<City>>(uri);
        }

        /// <summary>
        /// Получить города по имени
        /// </summary>
        public Task<IEnumerable<City>> GetCitiesByNameAsync(string name)
        {
            var builder = new UriBuilder(AppSettings.HotelsEndpoint);
            builder.AppendToPath("Cities");
            builder.Query = $"name={name}";

            var uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<City>>(uri);
        }

        /// <summary>
        /// Поиск отелей по городу
        /// </summary>
        public Task<IEnumerable<Models.Hotel>> SearchAsync(int cityId)
        {
            var builder = new UriBuilder(AppSettings.HotelsEndpoint);
            builder.AppendToPath("Hotels/search");
            builder.Query = $"cityId={cityId}";

            var uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Hotel>>(uri);
        }

        /// <summary>
        /// Поиск отелей по городу и заданным критериям
        /// </summary>
        public Task<IEnumerable<Models.Hotel>> SearchAsync(int cityId, int rating, int minPrice, int maxPrice)
        {
            var builder = new UriBuilder(AppSettings.HotelsEndpoint);
            builder.AppendToPath("Hotels/search");
            builder.Query = $"cityId={cityId}&rating={rating}&minPrice={minPrice}&maxPrice={maxPrice}";

            var uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Hotel>>(uri);
        }

        /// <summary>
        /// Получить самые посещаемые отели
        /// </summary>
        public Task<IEnumerable<Models.Hotel>> GetMostVisitedAsync()
        {
            var builder = new UriBuilder(AppSettings.HotelsEndpoint);
            builder.AppendToPath("Hotels/mostVisited");

            var uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Hotel>>(uri);
        }
        
        /// <summary>
        /// Отель по айди
        /// </summary>
        public Task<Models.Hotel> GetHotelByIdAsync(int id)
        {
            var builder = new UriBuilder(AppSettings.HotelsEndpoint);
            builder.AppendToPath($"Hotels/{id}");

            var uri = builder.ToString();

            return _requestService.GetAsync<Models.Hotel>(uri);
        }
        
        /// <summary>
        /// Отзывы отеля
        /// </summary>
        public Task<IEnumerable<Review>> GetReviewsAsync(int id)
        {
            var builder = new UriBuilder(AppSettings.HotelsEndpoint);
            builder.AppendToPath($"Reviews/{id}");

            var uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Review>>(uri);
        }

        /// <summary>
        /// Службы отеля
        /// </summary>
        public Task<IEnumerable<Service>> GetHotelServicesAsync()
        {
            var builder = new UriBuilder(AppSettings.HotelsEndpoint);
            builder.AppendToPath("Services/hotel");

            var uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Service>>(uri);
        }

        /// <summary>
        /// Службы номера
        /// </summary>
        public Task<IEnumerable<Service>> GetRoomServicesAsync()
        {
            var builder = new UriBuilder(AppSettings.HotelsEndpoint);
            builder.AppendToPath("Services/room");

            var uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Service>>(uri);
        }
    }
}