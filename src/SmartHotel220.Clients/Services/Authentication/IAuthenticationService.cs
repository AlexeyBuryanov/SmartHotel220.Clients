using SmartHotel220.Clients.Core.Models;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Authentication
{
    /// <summary>
    /// Описывает службу входа
    /// </summary>
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }

        User AuthenticatedUser { get; }

        Task<bool> LoginAsync(string email, string password);

        Task<bool> LoginWithMicrosoftAsync();

        Task<bool> UserIsAuthenticatedAndValidAsync();

        Task LogoutAsync();
    }
}
