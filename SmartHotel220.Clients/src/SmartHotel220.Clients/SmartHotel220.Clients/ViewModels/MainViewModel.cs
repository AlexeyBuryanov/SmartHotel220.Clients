using SmartHotel220.Clients.Core.ViewModels.Base;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Модель представлния меню
        /// </summary>
        private MenuViewModel _menuViewModel;

        public MainViewModel(MenuViewModel menuViewModel)
        {
            _menuViewModel = menuViewModel;
        }

        /// <summary>
        /// Модель представлния меню св-во
        /// </summary>
        public MenuViewModel MenuViewModel
        {
            get => _menuViewModel;
            set
            {
                _menuViewModel = value;
                OnPropertyChanged();
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            // Проводим инициализацию меню и после открываем главную страницу
            return Task.WhenAll
                (
                    _menuViewModel.InitializeAsync(navigationData),
                    NavigationService.NavigateToAsync<HomeViewModel>()
                );
        }
    }
}