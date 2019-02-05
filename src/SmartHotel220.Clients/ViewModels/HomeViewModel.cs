using SmartHotel220.Clients.Core.Models;
using SmartHotel220.Clients.Core.Services.Authentication;
using SmartHotel220.Clients.Core.Services.Booking;
using SmartHotel220.Clients.Core.Services.Chart;
using SmartHotel220.Clients.Core.Services.Notification;
using SmartHotel220.Clients.Core.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.ViewModels
{
    /// <summary>
    /// Дом / главная страница
    /// </summary>
    public class HomeViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        /// <summary>
        /// Есть ли бронь
        /// </summary>
        private bool _hasBooking;
        /// <summary>
        /// Диаграмма температуры
        /// </summary>
        private Microcharts.Chart _temperatureChart;
        /// <summary>
        /// Диаграмма времени уборки
        /// </summary>
        private Microcharts.Chart _greenChart;
        /// <summary>
        /// Уведомления
        /// </summary>
        private ObservableCollection<Notification> _notifications;

        /// <summary>
        /// Служба уведомлений
        /// </summary>
        private readonly INotificationService _notificationService;
        /// <summary>
        /// Служба диаграмм
        /// </summary>
        private readonly IChartService _chartService;
        /// <summary>
        /// Служба бронирования
        /// </summary>
        private readonly IBookingService _bookingService;
        /// <summary>
        /// Служба авторизации
        /// </summary>
        private readonly IAuthenticationService _authenticationService;

        public HomeViewModel(
            INotificationService notificationService,
            IChartService chartService,
            IBookingService bookingService,
            IAuthenticationService authenticationService)
        {
            _notificationService = notificationService;
            _chartService = chartService;
            _bookingService = bookingService;
            _authenticationService = authenticationService;
            _notifications = new ObservableCollection<Notification>();
        }

        /// <summary>
        /// Есть ли бронь св-во
        /// </summary>
        public bool HasBooking
        {
            get => _hasBooking;
            set
            {
                _hasBooking = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Диаграмма температуры св-во
        /// </summary>
        public Microcharts.Chart TemperatureChart
        {
            get => _temperatureChart;
            set
            {
                _temperatureChart = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// "Зелёная" диаграмма св-во
        /// </summary>
        public Microcharts.Chart GreenChart
        {
            get => _greenChart;
            set
            {
                _greenChart = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Уведомления св-во
        /// </summary>
        public ObservableCollection<Notification> Notifications
        {
            get => _notifications;
            set
            {
                _notifications = value;
                OnPropertyChanged();
            }
        }

        public ICommand NotificationsCommand => new AsyncCommand(OnNotificationsAsync);
        public ICommand OpenDoorCommand => new AsyncCommand(OpenDoorAsync);
        public ICommand BookRoomCommand => new AsyncCommand(BookRoomAsync);
        public ICommand SuggestionsCommand => new AsyncCommand(SuggestionsAsync);
        public ICommand BookConferenceCommand => new AsyncCommand(BookConferenceAsync);
        public ICommand GoMyRoomCommand => new AsyncCommand(GoMyRoomAsync);

        /// <summary>
        /// Инициализация
        /// </summary>
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                IsBusy = true;

                // Определяем имеется ли бронь
                HasBooking = AppSettings.HasBooking;

                // Создаём диаграммы
                TemperatureChart = await _chartService.GetTemperatureChartAsync();
                GreenChart = await _chartService.GetGreenChartAsync();

                // Пользователь
                var authenticatedUser = _authenticationService.AuthenticatedUser;
                // Уведомления
                var notifications = await _notificationService.GetNotificationsAsync(5, authenticatedUser.Token);
                Notifications = new ObservableCollection<Notification>(notifications);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Главная] Ошибка: {ex}");
                await DialogService.ShowAlertAsync(Resources.ExceptionMessage, Resources.ExceptionTitle, Resources.DialogOk);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// При появлении
        /// </summary>
        public Task OnViewAppearingAsync(VisualElement view)
        {
            // Подписываемся на события бронирования и выписки
            MessagingCenter.Subscribe<Booking>(this, MessengerKeys.BookingRequested, OnBookingRequested);
            MessagingCenter.Subscribe<CheckoutViewModel>(this, MessengerKeys.CheckoutRequested, OnCheckoutRequested);

            return Task.FromResult(true);
        }

        /// <summary>
        /// При исчезновении
        /// </summary>
        public Task OnViewDisappearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Уведомления
        /// </summary>
        private Task OnNotificationsAsync()
        {
            return NavigationService.NavigateToAsync(typeof(NotificationsViewModel), Notifications);
        }

        /// <summary>
        /// Открытие дверей
        /// </summary>
        private Task OpenDoorAsync()
        {
            return NavigationService.NavigateToPopupAsync<OpenDoorViewModel>(true);
        }

        /// <summary>
        /// Забронировать номер
        /// </summary>
        private Task BookRoomAsync()
        {
            return NavigationService.NavigateToAsync<BookingViewModel>();
        }

        /// <summary>
        /// Предложения
        /// </summary>
        private Task SuggestionsAsync()
        {
            return NavigationService.NavigateToAsync<SuggestionsViewModel>();
        }

        /// <summary>
        /// Забронировать зал
        /// </summary>
        private Task BookConferenceAsync()
        {
            return NavigationService.NavigateToAsync<BookingViewModel>();
        }

        /// <summary>
        /// Мой номер
        /// </summary>
        private Task GoMyRoomAsync()
        {
            if (HasBooking)
            {
                return NavigationService.NavigateToAsync<MyRoomViewModel>();
            }
            return Task.FromResult(true);
        }

        /// <summary>
        /// Вызывается при бронировании номера
        /// </summary>
        private void OnBookingRequested(Booking booking)
        {
            if (booking == null)
            {
                return;
            }

            HasBooking = true;
        }

        /// <summary>
        /// Вызывается при выписки
        /// </summary>
        private void OnCheckoutRequested(object args)
        {
            HasBooking = false;
        }
    }
}