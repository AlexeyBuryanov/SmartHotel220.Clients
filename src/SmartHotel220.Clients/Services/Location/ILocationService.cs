using SmartHotel220.Clients.Core.Models;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Location
{
    /// <summary>
    /// Описывает сервис по работе с гео-локацией
    /// </summary>
    public interface ILocationService
    {
        Task<GeoLocation> GetPositionAsync();
    }
}