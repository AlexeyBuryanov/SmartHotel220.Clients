using SmartHotel220.Clients.Core.Services.Analytic;
using SmartHotel220.Clients.Core.Services.Notification;
using SmartHotel220.Clients.Core.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Уведомления
    /// </summary>
    public class NotificationsViewModel : ViewModelBase
    {
        /// <summary>
        /// Служба уведомлений
        /// </summary>
        private readonly INotificationService _notificationService;
        /// <summary>
        /// Служба аналитики
        /// </summary>
        private readonly IAnalyticService _analyticService;

        /// <summary>
        /// Уведомления
        /// </summary>
        private ObservableCollection<Models.Notification> _notifications;
        /// <summary>
        /// Есть ли элементы
        /// </summary>
        private bool _hasItems;

        public NotificationsViewModel(
            INotificationService notificationService,
            IAnalyticService analyticService)
        {
            _notificationService = notificationService;
            _analyticService = analyticService;

            HasItems = true;
        }

        /// <summary>
        /// Список уведомлений св-во
        /// </summary>
        public ObservableCollection<Models.Notification> Notifications
        {
            get => _notifications;
            set
            {
                _notifications = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Есть ли элементы св-во
        /// </summary>
        public bool HasItems
        {
            get => _hasItems;
            set
            {
                _hasItems = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteNotificationCommand => new Command<Models.Notification>(OnDelete);

        /// <summary>
        /// Инициализация
        /// </summary>
        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                Notifications = (ObservableCollection<Models.Notification>)navigationData;
                HasItems = Notifications.Count > 0;
            }

            return base.InitializeAsync(navigationData);
        }

        /// <summary>
        /// При удалении
        /// </summary>
        /// <param name="notification">уведомление</param>
        private async void OnDelete(Models.Notification notification)
        {
            if (notification != null)
            {
                // Удаляем из интерфейса
                Notifications.Remove(notification);
                // Удаляем со службы
                await _notificationService.DeleteNotificationAsync(notification);
                // Фиксируем событие в аналитике
                _analyticService.TrackEvent("DeleteNotification");
                // Если уведомлений нет, говорим, что их нет
                HasItems = Notifications.Count > 0;
            }
        }
    }
}