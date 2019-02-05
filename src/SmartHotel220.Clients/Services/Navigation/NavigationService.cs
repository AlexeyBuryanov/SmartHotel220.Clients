using SmartHotel220.Clients.Core.Models;
using SmartHotel220.Clients.Core.Services.Authentication;
using SmartHotel220.Clients.Core.ViewModels;
using SmartHotel220.Clients.Core.ViewModels.Base;
using SmartHotel220.Clients.Core.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Services.Navigation
{
    /// <summary>
    /// Служба навигации
    /// </summary>
    public partial class NavigationService : INavigationService
    {
        /// <summary>
        /// Сервис аутентификации
        /// </summary>
        private readonly IAuthenticationService _authenticationService;
        /// <summary>
        /// Карта. Содержит в себе 2 типа - тип вью модели и представления
        /// </summary>
        protected readonly Dictionary<Type, Type> Mappings;

        /// <summary>
        /// Текущее кроссплатформ. приложение
        /// </summary>
        protected Application CurrentApplication => Application.Current;

        public NavigationService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            Mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {
            if (await _authenticationService.UserIsAuthenticatedAndValidAsync())
            {
                await NavigateToAsync<MainViewModel>();
            }
            else
            {
                await NavigateToAsync<LoginViewModel>();
            }
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is MainView mainPage)
            {
                await mainPage.Detail.Navigation.PopAsync();
            }
            else if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            if (CurrentApplication.MainPage is MainView mainPage)
            {
                mainPage.Detail.Navigation.RemovePage(
                    mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Внутренняя навигация к
        /// </summary>
        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            // Создаём и привязываем страницу
            var page = CreateAndBindPage(viewModelType, parameter);

            // Если это главный вид, то присваеваем нашему приложению главную страницу
            if (page is MainView)
            {
                CurrentApplication.MainPage = page;
            }
            // Если это логин создаём CustomNavigationPage на месте главной странице
            else if (page is LoginView)
            {
                CurrentApplication.MainPage = new CustomNavigationPage(page);
            }
            // Если приложение уже имеет главый вид (это master detail)
            else if (CurrentApplication.MainPage is MainView mainPage)
            {
                // И при этом детали это CustomNavigationPage
                if (mainPage.Detail is CustomNavigationPage navigationPage)
                {
                    // Текущая страница
                    var currentPage = navigationPage.CurrentPage;

                    // Если типы не совпадают с созданной ранее страницей
                    if (currentPage.GetType() != page.GetType())
                    {
                        // Показываем страницу модально
                        await navigationPage.PushAsync(page);
                    }
                }
                // Иначе, если детали главной страницы не соответствуют типу CustomNavigationPage
                else
                {
                    // Создаём страницу деталей
                    navigationPage = new CustomNavigationPage(page);
                    // Устанавливаем детали главному виду
                    mainPage.Detail = navigationPage;
                }

                mainPage.IsPresented = false;
            }
            // Всё равно нужно настроить главную страницу
            else
            {
                if (CurrentApplication.MainPage is CustomNavigationPage navigationPage)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(page);
                }
            }

            // Инициализация вью модели
            await ((ViewModelBase)page.BindingContext).InitializeAsync(parameter);
        }

        /// <summary>
        /// Получить тип страницы для вью модели
        /// </summary>
        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!Mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"Нет карты для ${viewModelType}");
            }

            return Mappings[viewModelType];
        }

        /// <summary>
        /// Создать и привязать страницу
        /// </summary>
        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Тип {viewModelType} не является страницей");
            }

            var page = Activator.CreateInstance(pageType) as Page;
            var viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            return page;
        }

        /// <summary>
        /// Создание карты
        /// </summary>
        private void CreatePageViewModelMappings()
        {
            Mappings.Add(typeof(BookingCalendarViewModel), typeof(BookingCalendarView));
            Mappings.Add(typeof(BookingHotelViewModel), typeof(BookingHotelView));
            Mappings.Add(typeof(BookingHotelsViewModel), typeof(BookingHotelsView));
            Mappings.Add(typeof(BookingViewModel), typeof(BookingView));
            Mappings.Add(typeof(CheckoutViewModel), typeof(CheckoutView));
            Mappings.Add(typeof(LoginViewModel), typeof(LoginView));
            Mappings.Add(typeof(MainViewModel), typeof(MainView));
            Mappings.Add(typeof(MyRoomViewModel), typeof(MyRoomView));
            Mappings.Add(typeof(NotificationsViewModel), typeof(NotificationsView));
            Mappings.Add(typeof(OpenDoorViewModel), typeof(OpenDoorView));
            Mappings.Add(typeof(SettingsViewModel<RemoteSettings>), typeof(SettingsView));
            Mappings.Add(typeof(ExtendedSplashViewModel), typeof(ExtendedSplashView));

            if (Device.Idiom == TargetIdiom.Desktop)
            {
                Mappings.Add(typeof(HomeViewModel), typeof(UwpHomeView));
                Mappings.Add(typeof(SuggestionsViewModel), typeof(UwpSuggestionsView));
            }
            else
            {
                Mappings.Add(typeof(HomeViewModel), typeof(HomeView));
                Mappings.Add(typeof(SuggestionsViewModel), typeof(SuggestionsView));
            }
        }
    }
}