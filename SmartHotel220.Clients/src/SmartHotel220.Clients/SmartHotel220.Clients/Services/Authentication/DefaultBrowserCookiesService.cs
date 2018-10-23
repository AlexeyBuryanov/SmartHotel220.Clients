using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Authentication
{
    public class DefaultBrowserCookiesService : IBrowserCookiesService
    {
        public Task ClearCookiesAsync()
        {
            return Task.FromResult(true);
        }
    }
}
