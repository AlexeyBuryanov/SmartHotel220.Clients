using Autofac;
using SmartHotel220.Clients.Core.Models;
using SmartHotel220.Clients.Core.Services.Analytic;
using SmartHotel220.Clients.Core.Services.Authentication;
using SmartHotel220.Clients.Core.Services.Booking;
using SmartHotel220.Clients.Core.Services.Chart;
using SmartHotel220.Clients.Core.Services.Dialog;
using SmartHotel220.Clients.Core.Services.Hotel;
using SmartHotel220.Clients.Core.Services.Location;
using SmartHotel220.Clients.Core.Services.Navigation;
using SmartHotel220.Clients.Core.Services.Notification;
using SmartHotel220.Clients.Core.Services.OpenUri;
using SmartHotel220.Clients.Core.Services.Request;
using SmartHotel220.Clients.Core.Services.Settings;
using SmartHotel220.Clients.Core.Services.Suggestion;
using System;

namespace SmartHotel220.Clients.Core.ViewModels.Base
{
    public class Locator
    {
        /// <summary>
        /// Контейнер зависимостей
        /// </summary>
        private IContainer _container;
        /// <summary>
        /// Для построения контейнера
        /// </summary>
        private readonly ContainerBuilder _containerBuilder;

        private static readonly Locator _instance = new Locator();
        /// <summary>
        /// Singleton
        /// </summary>
        public static Locator Instance => _instance;

        public Locator()
        {
            _containerBuilder = new ContainerBuilder();

            _containerBuilder.RegisterType<AnalyticService>().As<IAnalyticService>();
            _containerBuilder.RegisterType<DialogService>().As<IDialogService>();
            _containerBuilder.RegisterType<NavigationService>().As<INavigationService>();
            _containerBuilder.RegisterType<FakeChartService>().As<IChartService>();
            _containerBuilder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            _containerBuilder.RegisterType<LocationService>().As<ILocationService>();
            _containerBuilder.RegisterType<OpenUriService>().As<IOpenUriService>();
            _containerBuilder.RegisterType<RequestService>().As<IRequestService>();
            _containerBuilder.RegisterType<DefaultBrowserCookiesService>().As<IBrowserCookiesService>();
            _containerBuilder.RegisterType<GravatarUrlProvider>().As<IAvatarUrlProvider>();
            _containerBuilder.RegisterType(typeof(SettingsService)).As(typeof(ISettingsService<RemoteSettings>));

            if (AppSettings.UseFakes)
            {
                _containerBuilder.RegisterType<FakeBookingService>().As<IBookingService>();
                _containerBuilder.RegisterType<FakeHotelService>().As<IHotelService>();
                _containerBuilder.RegisterType<FakeNotificationService>().As<INotificationService>();
                _containerBuilder.RegisterType<FakeSuggestionService>().As<ISuggestionService>();
            }
            else
            {
                _containerBuilder.RegisterType<BookingService>().As<IBookingService>();
                _containerBuilder.RegisterType<HotelService>().As<IHotelService>();
                _containerBuilder.RegisterType<NotificationService>().As<INotificationService>();
                _containerBuilder.RegisterType<SuggestionService>().As<ISuggestionService>();
            }

            _containerBuilder.RegisterType<BookingCalendarViewModel>();
            _containerBuilder.RegisterType<BookingHotelViewModel>();
            _containerBuilder.RegisterType<BookingHotelsViewModel>();
            _containerBuilder.RegisterType<BookingViewModel>();
            _containerBuilder.RegisterType<CheckoutViewModel>();
            _containerBuilder.RegisterType<HomeViewModel>();
            _containerBuilder.RegisterType<LoginViewModel>();
            _containerBuilder.RegisterType<MainViewModel>();
            _containerBuilder.RegisterType<MenuViewModel>();
            _containerBuilder.RegisterType<MyRoomViewModel>();
            _containerBuilder.RegisterType<NotificationsViewModel>();
            _containerBuilder.RegisterType<OpenDoorViewModel>();
            _containerBuilder.RegisterType<SuggestionsViewModel>();

            _containerBuilder.RegisterType(typeof(SettingsViewModel<RemoteSettings>));
            _containerBuilder.RegisterType<ExtendedSplashViewModel>();
        }

        /// <summary>
        /// Получить ранее зарегистрированный сервис из контекста текущего контейнера
        /// </summary>
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// Получить ранее зарегистрированный сервис из контекста текущего контейнера по типу
        /// </summary>
        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        /// <summary>
        /// Регистрация типа
        /// </summary>
        /// <typeparam name="TInterface">Интерфейс</typeparam>
        /// <typeparam name="TImplementation">Реализация</typeparam>
        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _containerBuilder.RegisterType<TImplementation>().As<TInterface>();
        }

        /// <summary>
        /// Регистрация типа
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        public void Register<T>() where T : class
        {
            _containerBuilder.RegisterType<T>();
        }

        /// <summary>
        /// Построить новый контейнер с текущими компонентами регистрации
        /// </summary>
        public void Build()
        {
            _container = _containerBuilder.Build();
        }
    }
}