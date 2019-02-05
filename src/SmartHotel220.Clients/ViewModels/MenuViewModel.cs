using SmartHotel220.Clients.Core.Models;
using SmartHotel220.Clients.Core.Services.Authentication;
using SmartHotel220.Clients.Core.Services.OpenUri;
using SmartHotel220.Clients.Core.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.ViewModels
{
    /// <summary>
    /// Боковое меню
    /// </summary>
    public class MenuViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        // Строки мессенджеров
        private const string Skype = "Skype";
        private const string FacebookMessenger = "Facebook Messenger";
        private const string Telegram = "Telegram";

        /// <summary>
        /// Элементы меню
        /// </summary>
        private ObservableCollection<Models.MenuItem> _menuItems;

        /// <summary>
        /// Служба аутентификации
        /// </summary>
        private readonly IAuthenticationService _authenticationService;
        /// <summary>
        /// Служба открытия URI
        /// </summary>
        private readonly IOpenUriService _openUrlService;

        public MenuViewModel(
            IAuthenticationService authenticationService,
            IOpenUriService openUrlService)
        {
            _authenticationService = authenticationService;
            _openUrlService = openUrlService;

            MenuItems = new ObservableCollection<Models.MenuItem>();

            // Инициализация элементов
            InitMenuItems();
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName => AppSettings.User?.Name;
        /// <summary>
        /// Аватар, ссылка
        /// </summary>
        public string UserAvatar => AppSettings.User?.AvatarUrl;

        /// <summary>
        /// Элемент меню св-во
        /// </summary>
        public ObservableCollection<Models.MenuItem> MenuItems
        {
            get => _menuItems;
            set
            {
                _menuItems = value;
                OnPropertyChanged();
            }
        }

        public ICommand MenuItemSelectedCommand => new Command<Models.MenuItem>(OnSelectMenuItem);

        /// <summary>
        /// При появлении
        /// </summary>
        public Task OnViewAppearingAsync(VisualElement view)
        {
            // Подписываемся на событие бронирования и выписки
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
        /// Инициализация меню
        /// </summary>
        private void InitMenuItems()
        {
            MenuItems.Add(new Models.MenuItem
            {
                Title = "Домой",
                MenuItemType = MenuItemType.Home,
                ViewModelType = typeof(MainViewModel),
                IsEnabled = true
            });

            MenuItems.Add(new Models.MenuItem
            {
                Title = "Забронировать",
                MenuItemType = MenuItemType.BookRoom,
                ViewModelType = typeof(BookingViewModel),
                IsEnabled = true
            });

            MenuItems.Add(new Models.MenuItem
            {
                Title = "Мой номер",
                MenuItemType = MenuItemType.MyRoom,
                ViewModelType = typeof(MyRoomViewModel),
                IsEnabled = AppSettings.HasBooking
            });

            MenuItems.Add(new Models.MenuItem
            {
                Title = "Предложения",
                MenuItemType = MenuItemType.Suggestions,
                ViewModelType = typeof(SuggestionsViewModel),
                IsEnabled = true
            });

            MenuItems.Add(new Models.MenuItem
            {
                Title = "Консьерж",
                MenuItemType = MenuItemType.Concierge,
                IsEnabled = true
            });

            MenuItems.Add(new Models.MenuItem
            {
                Title = "Выйти",
                MenuItemType = MenuItemType.Logout,
                ViewModelType = typeof(LoginViewModel),
                IsEnabled = true,
                AfterNavigationAction = RemoveUserCredentials
            });
        }

        /// <summary>
        /// При выборе меню
        /// </summary>
        private async void OnSelectMenuItem(Models.MenuItem item)
        {
            // Если выбран консьерж
            if (item.MenuItemType == MenuItemType.Concierge)
            {
                // Если это десктоп, то запускаем скайп-бот
                if (Device.RuntimePlatform == Device.UWP)
                {
                    _openUrlService.OpenSkypeBot(AppSettings.SkypeBotId);
                }
                else
                {
                    await OpenBotAsync();
                }
            }
            else if (item.IsEnabled && item.ViewModelType != null)
            {
                // Вызываем действие после навигации
                item.AfterNavigationAction?.Invoke();
                // Переместится к модели представления по типу
                await NavigationService.NavigateToAsync(item.ViewModelType, item);
            }
        }

        /// <summary>
        /// Удаление учетных данных пользователя
        /// </summary>
        private Task RemoveUserCredentials()
        {
            AppSettings.HasBooking = false;

            // Посылаем сообщение о выписке
            MessagingCenter.Send(this, MessengerKeys.CheckoutRequested);

            // Выходим
            return _authenticationService.LogoutAsync();
        }

        /// <summary>
        /// При выполнении бронирования
        /// </summary>
        private void OnBookingRequested(Booking booking)
        {
            if (booking == null)
            {
                return;
            }

            SetMenuItemStatus(MenuItemType.MyRoom, true);
        }

        /// <summary>
        /// При выписке
        /// </summary>
        private void OnCheckoutRequested(object args)
        {
            SetMenuItemStatus(MenuItemType.MyRoom, false);
        }

        /// <summary>
        /// Изменить статус элемента бокового меню
        /// </summary>
        private void SetMenuItemStatus(MenuItemType type, bool enabled)
        {
            var menuItem = MenuItems.FirstOrDefault(m => m.MenuItemType == type);

            if (menuItem != null)
            {
                menuItem.IsEnabled = enabled;
            }
        }

        /// <summary>
        /// Открыть бота
        /// </summary>
        private async Task OpenBotAsync()
        {
            await Task.Delay(100);

            var bots = new[] { Skype, FacebookMessenger, Telegram };

            try
            {
                var selectedBot =
                    await DialogService.SelectActionAsync(
                        Resources.BotSelectionMessage,
                        Resources.BotSelectionTitle,
                        bots);

                switch (selectedBot)
                {
                    case Skype:
                        _openUrlService.OpenSkypeBot(AppSettings.SkypeBotId);
                        break;
                    case FacebookMessenger:
                        _openUrlService.OpenFacebookBot(AppSettings.FacebookBotId);
                        break;
                    case Telegram:
                        _openUrlService.OpenFacebookBot(AppSettings.TelegramBotId);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Открытие бота: {ex}");
            }
        }
    }
}