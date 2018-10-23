using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Authentication
{
    /// <summary>
    /// Описывает службу по работе с куки
    /// </summary>
    public interface IBrowserCookiesService
    {
        Task ClearCookiesAsync();
    }
}
