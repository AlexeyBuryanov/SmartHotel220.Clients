using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Hotel
{
    /// <summary>
    /// Описывает службу по работе с отелями
    /// </summary>
    public interface IHotelService
    {
        /// <summary>
        /// Получить города
        /// </summary>
        Task<IEnumerable<Models.City>> GetCitiesAsync();

        /// <summary>
        /// Получить города по имени
        /// </summary>
        Task<IEnumerable<Models.City>> GetCitiesByNameAsync(string name);

        /// <summary>
        /// Поиск отелей в городе
        /// </summary>
        Task<IEnumerable<Models.Hotel>> SearchAsync(int cityId);

        /// <summary>
        /// Поиск отелей в городе по рейтингу и цене
        /// </summary>
        Task<IEnumerable<Models.Hotel>> SearchAsync(int cityId, int rating, int minPrice, int maxPrice);

        /// <summary>
        /// Самые популярные
        /// </summary>
        Task<IEnumerable<Models.Hotel>> GetMostVisitedAsync();

        /// <summary>
        /// Получить отель по ID
        /// </summary>
        Task<Models.Hotel> GetHotelByIdAsync(int id);

        /// <summary>
        /// Получить отзывы
        /// </summary>
        Task<IEnumerable<Models.Review>> GetReviewsAsync(int id);

        /// <summary>
        /// Получить сервисы отелей
        /// </summary>
        Task<IEnumerable<Models.Service>> GetHotelServicesAsync();

        /// <summary>
        /// Получить сервисы номеров
        /// </summary>
        Task<IEnumerable<Models.Service>> GetRoomServicesAsync();
    }
}