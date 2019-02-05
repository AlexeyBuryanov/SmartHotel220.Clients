using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using SmartHotel220.Clients.Core.Models;
using SmartHotel220.Clients.Core.Services.Analytic;
using SmartHotel220.Clients.Core.Services.Authentication;
using SmartHotel220.Clients.Core.Services.NFC;
using SmartHotel220.Clients.Core.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Открытие дверей
    /// </summary>
    public class OpenDoorViewModel : ViewModelBase
    {
        /// <summary>
        /// Служба аутентификации
        /// </summary>
        private readonly IAuthenticationService _authenticationService;
        /// <summary>
        /// Служба NFC
        /// </summary>
        private readonly INfcService _nfcService;
        /// <summary>
        /// Служба аналитики
        /// </summary>
        private readonly IAnalyticService _analyticService;

        public OpenDoorViewModel(
            IAuthenticationService authenticationService,
            IAnalyticService analyticService)
        {
            _authenticationService = authenticationService;
            _analyticService = analyticService;
            _nfcService = DependencyService.Get<INfcService>();
        }

        public ICommand ClosePopupCommand => new AsyncCommand(ClosePopupAsync);

        /// <summary>
        /// Инициализация
        /// </summary>
        public override async Task InitializeAsync(object navigationData)
        {
            // Служба NFC должна быть создана
            if (_nfcService != null)
            {
                // NFC должен быть включён
                if (!_nfcService.IsAvailable)
                {
                    await DialogService.ShowAlertAsync(Resources.NoNfc, Resources.Information, Resources.DialogOk);
                    return;
                }

                // Пользователь
                var authenticatedUser = _authenticationService.AuthenticatedUser;

                var nfcParameter = new NfcParameter
                {
                    Username = authenticatedUser.Name,
                    Avatar = authenticatedUser.AvatarUrl
                };

                var serializedMessage = JsonConvert.SerializeObject(nfcParameter);

                // Посылаем сообщение об открытии дверей
                MessagingCenter.Send(serializedMessage, MessengerKeys.SendNFCToken);
                // Отправляем данные в аналитику
                _analyticService.TrackEvent("OpenDoor");
            }
        }

        private Task ClosePopupAsync()
        {
            return PopupNavigation.PopAllAsync();
        }
    }
}