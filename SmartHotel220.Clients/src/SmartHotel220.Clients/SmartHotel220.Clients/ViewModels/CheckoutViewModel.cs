using Rg.Plugins.Popup.Services;
using SmartHotel220.Clients.Core.Services.Analytic;
using SmartHotel220.Clients.Core.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Выписка
    /// </summary>
    public class CheckoutViewModel : ViewModelBase
    {
        /// <summary>
        /// Служба аналитики
        /// </summary>
        private readonly IAnalyticService _analyticService;

        public CheckoutViewModel(IAnalyticService analyticService)
        {
            _analyticService = analyticService;
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName => AppSettings.User?.Name;
        /// <summary>
        /// Аватар, ссылка
        /// </summary>
        public string UserAvatar => AppSettings.User?.AvatarUrl;

        public ICommand ClosePopupCommand => new AsyncCommand(ClosePopupAsync);
        public ICommand CheckoutCommand => new AsyncCommand(CheckoutAsync);

        /// <summary>
        /// Закрытие Popup
        /// </summary>
        private Task ClosePopupAsync()
        {
            // Убираем бронь
            AppSettings.HasBooking = false;

            // Посылаем сообщение, что прошла выписка
            MessagingCenter.Send(this, MessengerKeys.CheckoutRequested);
            // Фиксируем событие в аналитике
            _analyticService.TrackEvent("Checkout");

            return PopupNavigation.PopAllAsync();
        }

        /// <summary>
        /// Сделать ещё одно бронирование с выпиской
        /// </summary>
        private async Task CheckoutAsync()
        {
            // Убираем бронь
            AppSettings.HasBooking = false;

            // Посылаем сообщение, что прошла выписка
            MessagingCenter.Send(this, MessengerKeys.CheckoutRequested);
            // Фиксируем событие в аналитике
            _analyticService.TrackEvent("Checkout");

            await PopupNavigation.PopAllAsync(false);
    
            // Перемещаемся на начальную страницу бронирования
            await NavigationService.NavigateToAsync<BookingViewModel>();
        }
    }
}