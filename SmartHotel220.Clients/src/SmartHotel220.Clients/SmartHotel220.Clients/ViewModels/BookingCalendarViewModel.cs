using SmartHotel220.Clients.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Бронирование. Календарь
    /// </summary>
    public class BookingCalendarViewModel : ViewModelBase
    {
        /// <summary>
        /// Выбранный город
        /// </summary>
        private Models.City _city;
        /// <summary>
        /// Выбранные даты
        /// </summary>
        private ObservableCollection<DateTime> _dates;
        /// <summary>
        /// Дата от
        /// </summary>
        private DateTime _from;
        /// <summary>
        /// Дата до
        /// </summary>
        private DateTime _until;
        /// <summary>
        /// Можно ли переходить дальше
        /// </summary>
        private bool _isNextEnabled;

        public BookingCalendarViewModel()
        {
            var today = DateTime.Today;

            _dates = new ObservableCollection<DateTime>
            {
                today
            };

            // Выбираем сегодняшнюю дату
            SelectedDate(today);
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
        /// Даты св-во
        /// </summary>
        public ObservableCollection<DateTime> Dates
        {
            get => _dates;
            set
            {
                _dates = value;
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
        /// Можно ли двигаться дальше св-во
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

        public ICommand SelectedDateCommand => new Command(SelectedDate);
        public ICommand NextCommand => new AsyncCommand(NextAsync);

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="navigationData">Параметр при навигации</param>
        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                City = navigationData as Models.City;
            }

            return base.InitializeAsync(navigationData);
        }

        /// <summary>
        /// Выбранная дата
        /// </summary>
        private void SelectedDate(object date)
        {
            if (date == null)
                return;

            // Если есть хоть какие-то даты, то заполняем св-ва
            if (Dates.Any())
            {
                From = Dates.OrderBy(d => d.Day).FirstOrDefault();
                Until = Dates.OrderBy(d => d.Day).LastOrDefault();
                IsNextEnabled = Dates.Any();
            }
        }

        /// <summary>
        /// "Дальше"
        /// </summary>
        private Task NextAsync()
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "city", City },
                { "from", From },
                { "until", Until }
            };

            return NavigationService.NavigateToAsync<BookingHotelsViewModel>(navigationParameter);
        }
    }
}