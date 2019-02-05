using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Элемент меню
    /// </summary>
    public class MenuItem : BindableObject
    {
        private string _title;
        private MenuItemType _menuItemType;
        private Type _viewModelType;
        private bool _isEnabled;

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title
        {
            get => _title;

            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Тип меню
        /// </summary>
        public MenuItemType MenuItemType
        {
            get => _menuItemType;

            set
            {
                _menuItemType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Тип модели представления
        /// </summary>
        public Type ViewModelType
        {
            get => _viewModelType;

            set
            {
                _viewModelType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Включен ли
        /// </summary>
        public bool IsEnabled
        {
            get => _isEnabled;

            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Действие после навигации
        /// </summary>
        public Func<Task> AfterNavigationAction { get; set; }
    }
}