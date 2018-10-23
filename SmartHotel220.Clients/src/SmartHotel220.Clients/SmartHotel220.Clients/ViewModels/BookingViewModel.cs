using SmartHotel220.Clients.Core.Services.Analytic;
using SmartHotel220.Clients.Core.Services.DismissKeyboard;
using SmartHotel220.Clients.Core.Services.Hotel;
using SmartHotel220.Clients.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
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
    /// Бронирование
    /// </summary>
    public class BookingViewModel : ViewModelBase
    {
        /// <summary>
        /// Служба аналитики
        /// </summary>
        private readonly IAnalyticService _analyticService;
        /// <summary>
        /// Служба отелей
        /// </summary>
        private readonly IHotelService _hotelService;

        /// <summary>
        /// Строка поиска
        /// </summary>
        private string _search;
        /// <summary>
        /// Города
        /// </summary>
        private IEnumerable<Models.City> _cities;
        /// <summary>
        /// Города по дефолту
        /// </summary>
        private IEnumerable<Models.City> _citiesDefault;
        /// <summary>
        /// Отображающийся список предложений
        /// </summary>
        private IEnumerable<string> _suggestions;
        /// <summary>
        /// Строка предложения
        /// </summary>
        private string _suggestion;
        /// <summary>
        /// Можно ли идти дальше
        /// </summary>
        private bool _isNextEnabled;

        public BookingViewModel(
            IAnalyticService analyticService,
            IHotelService hotelService)
        {
            _analyticService = analyticService;
            _hotelService = hotelService;

            _cities = new List<Models.City>();
            _citiesDefault = new List<Models.City>();
            _suggestions = new List<string>();
        }

        /// <summary>
        /// Поиск св-во
        /// </summary>
        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                FilterAsync(_search);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Список предложений св-во
        /// </summary>
        public IEnumerable<string> Suggestions
        {
            get => _suggestions;
            set
            {
                _suggestions = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Предложение св-во
        /// </summary>
        public string Suggestion
        {
            get => _suggestion;
            set
            {
                _suggestion = value;
                
                IsNextEnabled = !string.IsNullOrEmpty(_suggestion);

                // Закрываем клавиатуру
                var dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();
                dismissKeyboardService.DismissKeyboard();

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Можно ли идти дальше св-во
        /// </summary>
        public bool IsNextEnabled
        {
            get => _isNextEnabled;
            set
            {
                _isNextEnabled = value;
                OnPropertyChanged();
            }
        }

        public ICommand NextCommand => new AsyncCommand(NextAsync);

        /// <summary>
        /// Инициализация
        /// </summary>
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                IsBusy = true;

                // Получаем города
                _cities = await _hotelService.GetCitiesAsync();
                _citiesDefault = _cities;

                Suggestions = new List<string>(_cities.Select(c => c.ToString()));
            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"[Бронирование. Где] Ошибка при получении данных: {httpEx}");

                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    await DialogService.ShowAlertAsync(
                        string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                        Resources.HttpRequestExceptionTitle,
                        Resources.DialogOk);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Бронирование. Где] Ошибка: {ex}");

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

        /// <summary>
        /// Фильтрация
        /// </summary>
        /// <param name="search">Заданая строка поиска</param>
        private async void FilterAsync(string search)
        {
            try
            {
                IsBusy = true;

                // Если был пустой запрос, то возвращаем список городов по умолчанию
                if (string.IsNullOrWhiteSpace(search))
                {
                    _cities = _citiesDefault;
                }

                Suggestions = new List<string>(
                    _cities.Select(c => c.ToString())
                           .Where(c => c.ToLowerInvariant()
                           .Contains(search.ToLowerInvariant())));

                // Если ничего не нашло вариантах по дефолту, то ищем в API
                if (!Suggestions.Any())
                {
                    try
                    {
                        IsBusy = true;

                        _cities = await _hotelService.GetCitiesByNameAsync(search);

                        Suggestions = new List<string>(
                            _cities.Select(c => c.ToString())
                                .Where(c => c.ToLowerInvariant()
                                .Contains(search.ToLowerInvariant())));
                    }
                    catch (HttpRequestException httpEx)
                    {
                        Debug.WriteLine($"[Бронирование. Где] Ошибка при получении данных: {httpEx}");

                        if (!string.IsNullOrEmpty(httpEx.Message))
                        {
                            await DialogService.ShowAlertAsync(
                                string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                                Resources.HttpRequestExceptionTitle,
                                Resources.DialogOk);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"[Бронирование. Где] Ошибка: {ex}");

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

                // Запечатляем событие в аналитике App Center
                _analyticService.TrackEvent("Filter", new Dictionary<string, string>
                {
                    { "Search", search }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Бронирование] Ошибка: {ex}");
                await DialogService.ShowAlertAsync(Resources.ExceptionMessage, Resources.ExceptionTitle, Resources.DialogOk);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Дальше
        /// </summary>
        private Task NextAsync()
        {
            var city = _cities.FirstOrDefault(c => c.ToString()
                              .Equals(Suggestion));

            if (city != null)
            {
                // Переходим в календарь
                return NavigationService.NavigateToAsync<BookingCalendarViewModel>(city);
            }

            // Просто возвращаем задачу
            return Task.FromResult(true);
        }
    }
}