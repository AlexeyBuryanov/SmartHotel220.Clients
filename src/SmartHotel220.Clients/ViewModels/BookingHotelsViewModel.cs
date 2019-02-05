using SmartHotel220.Clients.Core.Extensions;
using SmartHotel220.Clients.Core.Services.Hotel;
using SmartHotel220.Clients.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Бронирование. Доступные отели
    /// </summary>
    public class BookingHotelsViewModel : ViewModelBase
    {
        /// <summary>
        /// Отели
        /// </summary>
        private ObservableCollection<Models.Hotel> _hotels;
        /// <summary>
        /// Город
        /// </summary>
        private Models.City _city;
        /// <summary>
        /// Дата от
        /// </summary>
        private DateTime _from;
        /// <summary>
        /// Дата до
        /// </summary>
        private DateTime _until;

        /// <summary>
        /// Служба отелей
        /// </summary>
        private readonly IHotelService _hotelService;

        public BookingHotelsViewModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        /// <summary>
        /// Город св-во
        /// </summary>
        public Models.City City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// От св-во
        /// </summary>
        public DateTime From
        {
            get => _from;
            set
            {
                _from = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// До св-во
        /// </summary>
        public DateTime Until
        {
            get => _until;
            set
            {
                _until = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Отели св-во
        /// </summary>
        public ObservableCollection<Models.Hotel> Hotels
        {
            get => _hotels;
            set
            {
                _hotels = value;
                OnPropertyChanged();
            }
        }

        public ICommand HotelSelectedCommand => new Command<Models.Hotel>(OnSelectHotelAsync);

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="navigationData">Переданные данные</param>
        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                var navigationParameter = navigationData as Dictionary<string, object>;
                City = navigationParameter["city"] as Models.City;
                From = (DateTime)navigationParameter["from"];
                Until = (DateTime)navigationParameter["until"];
            }

            try
            {
                // Отображаем загрузку
                IsBusy = true;

                // Ищем отели
                var hotels = await _hotelService.SearchAsync(City.Id);
                Hotels = hotels.ToObservableCollection();
            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"[Бронирование. Отели] Ошибка при получении данных: {httpEx}");

                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    // Отображаем диалог с ошибкой
                    await DialogService.ShowAlertAsync(
                        string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                        Resources.HttpRequestExceptionTitle,
                        Resources.DialogOk);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Бронирование. Отели] Ошибка: {ex}");

                // Отображаем диалог с ошибкой
                await DialogService.ShowAlertAsync(
                    Resources.ExceptionMessage,
                    Resources.ExceptionTitle,
                    Resources.DialogOk);
            }
            finally
            {
                // Убираем загрузку
                IsBusy = false;
            }
        }

        /// <summary>
        /// При выборе отеля
        /// </summary>
        /// <param name="item">Отель</param>
        private async void OnSelectHotelAsync(Models.Hotel item)
        {
            if (item != null)
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { "hotel", item },
                    { "from", From },
                    { "until", Until }
                };

                await NavigationService.NavigateToAsync<BookingHotelViewModel>(navigationParameter);
            }
        }
    }
}