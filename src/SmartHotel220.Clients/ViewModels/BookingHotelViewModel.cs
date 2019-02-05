using SmartHotel220.Clients.Core.Extensions;
using SmartHotel220.Clients.Core.Services.Authentication;
using SmartHotel220.Clients.Core.Services.Booking;
using SmartHotel220.Clients.Core.Services.Hotel;
using SmartHotel220.Clients.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Бронирование. Страница отеля
    /// </summary>
    public class BookingHotelViewModel : ViewModelBase
    {
        // Активная опция на данный момент
        private bool _myHotel;
        private bool _rooms;
        private bool _reviews;
        /// <summary>
        /// Отель текущий
        /// </summary>
        private Models.Hotel _hotel;
        /// <summary>
        /// Дата от
        /// </summary>
        private DateTime _from;
        /// <summary>
        /// Дата до
        /// </summary>
        private DateTime _until;
        /// <summary>
        /// Сервисы отеля
        /// </summary>
        private ObservableCollection<Models.Service> _hotelServices;
        /// <summary>
        /// Сервисы номера
        /// </summary>
        private ObservableCollection<Models.Service> _roomServices;
        /// <summary>
        /// Отзывы
        /// </summary>
        private ObservableCollection<Models.Review> _hotelReviews;

        /// <summary>
        /// Служба отелей
        /// </summary>
        private readonly IHotelService _hotelService;
        /// <summary>
        /// Служба бронирования
        /// </summary>
        private readonly IBookingService _bookingService;
        /// <summary>
        /// Служба аутентификации
        /// </summary>
        private readonly IAuthenticationService _authenticationService;

        public BookingHotelViewModel(
            IHotelService hotelService,
            IBookingService bookingService,
            IAuthenticationService authenticationService)
        {
            _hotelService = hotelService;
            _bookingService = bookingService;
            _authenticationService = authenticationService;

            SetMyHotel();
        }

        /// <summary>
        /// Отель св-во
        /// </summary>
        public Models.Hotel Hotel
        {
            get => _hotel;
            set
            {
                _hotel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Сервисы отеля св-во
        /// </summary>
        public ObservableCollection<Models.Service> HotelServices
        {
            get => _hotelServices;
            set
            {
                _hotelServices = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Сервисы номера св-во
        /// </summary>
        public ObservableCollection<Models.Service> RoomServices
        {
            get => _roomServices;
            set
            {
                _roomServices = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Отзывы отеля св-во
        /// </summary>
        public ObservableCollection<Models.Review> HotelReviews
        {
            get => _hotelReviews;
            set
            {
                _hotelReviews = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Опция мой отель св-во
        /// </summary>
        public bool MyHotel
        {
            get => _myHotel;
            set
            {
                _myHotel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Опция номера св-во
        /// </summary>
        public bool Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Отзывы св-во
        /// </summary>
        public bool Reviews
        {
            get => _reviews;
            set
            {
                _reviews = value;
                OnPropertyChanged();
            }
        }

        public ICommand MyHotelCommand => new Command(SetMyHotel);
        public ICommand RoomsCommand => new Command(SetRooms);
        public ICommand ReviewsCommand => new Command(SetReviews);
        public ICommand BookingCommand => new AsyncCommand(BookingAsync);

        /// <summary>
        /// Инициализация
        /// </summary>
        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                var navigationParameter = navigationData as Dictionary<string, object>;
                Hotel = navigationParameter["hotel"] as Models.Hotel;
                _from = (DateTime)navigationParameter["from"];
                _until = (DateTime)navigationParameter["until"];

                if (Hotel != null)
                {
                    try
                    {
                        IsBusy = true;

                        // Получаем отель
                        Hotel = await _hotelService.GetHotelByIdAsync(Hotel.Id);

                        // Получаем сервисы отеля
                        var hotelServices = await _hotelService.GetHotelServicesAsync();
                        HotelServices = hotelServices.ToObservableCollection();

                        // Получаем сервисы номера
                        var roomServices = await _hotelService.GetRoomServicesAsync();
                        RoomServices = roomServices.ToObservableCollection();

                        // Получаем отзывы
                        var hotelReviews = await _hotelService.GetReviewsAsync(Hotel.Id);
                        HotelReviews = hotelReviews.ToObservableCollection();

                        // Получаем степень заполненности номеров
                        if (Hotel.Rooms.Any())
                        {
                            await GetOccupancyAsync();
                        }
                    }
                    catch (HttpRequestException httpEx)
                    {
                        Debug.WriteLine($"[Бронирование. Отель] Ошибка при получении данных: {httpEx}");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"[Бронирование. Отель] Error: {ex}");

                        await DialogService.ShowAlertAsync(
                            Resources.ExceptionMessage,
                            Resources.ExceptionTitle,
                            Resources.DialogOk);
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                }
            }
        }

        private void SetMyHotel()
        {
            MyHotel = true;
            Rooms = false;
            Reviews = false;
        }
        private void SetRooms()
        {
            MyHotel = false;
            Rooms = true;
            Reviews = false;
        }
        private void SetReviews()
        {
            MyHotel = false;
            Rooms = false;
            Reviews = true;
        }

        /// <summary>
        /// Бронирование
        /// </summary>
        private async Task BookingAsync()
        {
            try
            {
                // Пользователь
                var authenticatedUser = _authenticationService.AuthenticatedUser;

                // Диалог подтверждения
                var booking = await DialogService.ShowConfirmAsync(
                    string.Format(Resources.DialogBookingMessage, Hotel.Name),
                    Resources.DialogBookingTitle,
                    Resources.DialogYes,
                    Resources.DialogNo);

                // Если да 
                if (booking)
                {
                    // Разница дней бронирования
                    var diff = (_until - _from).Days;
                    // Общая сумма за бронь
                    var summaryPrice = diff * Hotel.PricePerNight;

                    // Формируем новое бронирование
                    var newBooking = new Models.Booking
                    {
                        UserId = authenticatedUser.Email,
                        HotelId = Hotel.Id,
                        Adults = 1,
                        Babies = 0,
                        Kids = 0,
                        Price = summaryPrice,
                        Rooms = new List<Models.Room>
                        {
                            new Models.Room { Quantity = 1, RoomType = 0 }
                        },
                        From = _from,
                        To = _until
                    };

                    // Создаём
                    await _bookingService.CreateBookingAsync(newBooking, authenticatedUser.Token);

                    // Запоминаем
                    AppSettings.HasBooking = true;

                    // Перемещаемся на главную
                    await NavigationService.NavigateToAsync<MainViewModel>();

                    // Посылаем сообщение о том, что бронирование создаон
                    MessagingCenter.Send(newBooking, MessengerKeys.BookingRequested);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Бронирование] Ошибка: {ex}");

                await DialogService.ShowAlertAsync(
                    Resources.ExceptionMessage,
                    Resources.ExceptionTitle,
                    Resources.DialogOk);
            }
        }

        /// <summary>
        /// Получить заполненность номеров в отеле
        /// </summary>
        private async Task GetOccupancyAsync()
        {
            // Выбираем первый номер
            var room = Hotel.Rooms.First();
            // Проверяем заполненность
            var occupancy = await _bookingService.GetOccupancyInformationAsync(room.RoomId, _until);
            // Солнечная погода
            var ocuppancyIfSunny = occupancy.OcuppancyIfSunny;

            string toast;

            if (ocuppancyIfSunny <= (100 / 3))
            {
                toast = Resources.LowOccupancy;
            }
            else if (ocuppancyIfSunny > (100 / 3) && ocuppancyIfSunny <= (100 / 1.5))
            {
                toast = Resources.MediumOccupancy;
            }
            else
            {
                toast = Resources.HighOccupancy;
            }

            // Показываем toast-message
            if (!string.IsNullOrEmpty(toast))
            {
                DialogService.ShowToast(toast);
            }
        }
    }
}