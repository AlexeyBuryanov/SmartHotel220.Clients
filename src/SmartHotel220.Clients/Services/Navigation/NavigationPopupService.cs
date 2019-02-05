using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SmartHotel220.Clients.Core.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Navigation
{
    /// <summary>
    /// Часть службы навигации. Всплывающие окна
    /// </summary>
    public partial class NavigationService
    {
        public Task NavigateToPopupAsync<TViewModel>(bool animate) where TViewModel : ViewModelBase
        {
            return NavigateToPopupAsync<TViewModel>(null, animate);
        }

        public async Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate) where TViewModel : ViewModelBase
        {
            // Создаём и привязываем страницу
            var page = CreateAndBindPage(typeof(TViewModel), parameter);
            // Инициализация вью модели
            await ((ViewModelBase)page.BindingContext).InitializeAsync(parameter);

            // Если PopupPage
            if (page is PopupPage popupPage)
            {
                await PopupNavigation.PushAsync(popupPage, animate);
            }
            else
            {
                throw new ArgumentException($"Тип ${typeof(TViewModel)} - это не PopupPage");
            }
        }
    }
}