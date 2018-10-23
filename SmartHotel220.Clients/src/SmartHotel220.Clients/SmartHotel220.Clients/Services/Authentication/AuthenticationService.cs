using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Internal;
using System;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Authentication
{
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Сервис куки
        /// </summary>
        private readonly IBrowserCookiesService _browserCookiesService;
        /// <summary>
        /// Поставщик аватара
        /// </summary>
        private readonly IAvatarUrlProvider _avatarProvider;

        public AuthenticationService(IBrowserCookiesService browserCookiesService, IAvatarUrlProvider avatarProvider)
        {
            _browserCookiesService = browserCookiesService;
            _avatarProvider = avatarProvider;
        }

        /// <summary>
        /// Аутентифицирован ли
        /// </summary>
        public bool IsAuthenticated => AppSettings.User != null;

        /// <summary>
        /// Пользователь, который вошёл
        /// </summary>
        public Models.User AuthenticatedUser => AppSettings.User;

        /// <summary>
        /// Войти
        /// </summary>
        public Task<bool> LoginAsync(string email, string password)
        {
            var user = new Models.User
            {
                Email = email,
                Name = email,
                LastName = string.Empty,
                AvatarUrl = _avatarProvider.GetAvatarUrl(email),
                Token = email,
                LoggedInWithMicrosoftAccount = false
            };

            AppSettings.User = user;

            return Task.FromResult(true);
        }

        /// <summary>
        /// Войти через AADB2C
        /// </summary>
        public async Task<bool> LoginWithMicrosoftAsync()
        {
            var succeeded = false;

            try
            {
                var result = await App.AuthenticationClient.AcquireTokenAsync(
                    new[] { AppSettings.B2cClientId },
                    string.Empty,
                    UiOptions.SelectAccount,
                    string.Empty,
                    null,
                    $"{AppSettings.B2cAuthority}{AppSettings.B2cTenant}",
                    AppSettings.B2cPolicy);

                var user = AuthenticationResultHelper.GetUserFromResult(result);
                user.AvatarUrl = _avatarProvider.GetAvatarUrl(user.Email);
                user.LoggedInWithMicrosoftAccount = true;
                AppSettings.User = user;

                succeeded = true;
            }
            catch (MsalException ex)
            {
                if (ex.ErrorCode != MsalError.AuthenticationCanceled)
                {
                    System.Diagnostics.Debug.WriteLine($"Ошибка при аутентификации MSAL: {ex}");
                    throw new ServiceAuthenticationException();
                }
            }

            return succeeded;
        }
        
        /// <summary>
        /// Пользователь вошёл и валидный
        /// </summary>
        public async Task<bool> UserIsAuthenticatedAndValidAsync()
        {
            if (!IsAuthenticated)
            {
                return false;
            }

            if (!AuthenticatedUser.LoggedInWithMicrosoftAccount)
            {
                return true;
            }

            var refreshSucceded = false;

            try
            {
                var tokenCache = App.AuthenticationClient.UserTokenCache;
                var ar = await App.AuthenticationClient.AcquireTokenSilentAsync(
                    new[] { AppSettings.B2cClientId },
                    AuthenticatedUser.Id,
                    $"{AppSettings.B2cAuthority}{AppSettings.B2cTenant}",
                    AppSettings.B2cPolicy,
                    true);

                SaveAuthenticationResult(ar);

                refreshSucceded = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при аутентификации MSAL. Попытка обновления: {ex}");
            }

            return refreshSucceded;
        }

        /// <summary>
        /// Выход
        /// </summary>
        public async Task LogoutAsync()
        {
            AppSettings.RemoveUserData();
            await _browserCookiesService.ClearCookiesAsync();
        }

        /// <summary>
        /// Сохранить результат входа
        /// </summary>
        private void SaveAuthenticationResult(AuthenticationResult result)
        {
            var user = AuthenticationResultHelper.GetUserFromResult(result);
            user.AvatarUrl = _avatarProvider.GetAvatarUrl(user.Email);
            AppSettings.User = user;
        }
    }
}
