using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Microsoft.Identity.Client;
using SmartHotel220.Clients.Core;
using SmartHotel220.Clients.Core.Services.Navigation;
using SmartHotel220.Clients.Core.ViewModels;
using SmartHotel220.Clients.Core.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SmartHotel220.Clients
{
    public partial class App
    {
        /// <summary>
        /// Клиент
        /// </summary>
        public static PublicClientApplication AuthenticationClient { get; set; }

        static App()
        {
            BuildDependencies();
        }
        public App()
        {
            AuthenticationClient =
                new PublicClientApplication($"{AppSettings.B2cAuthority}{AppSettings.B2cTenant}", AppSettings.B2cClientId);

            InitializeComponent();
    
            // Инициализируем навигацию (асинхронно)
            InitNavigation();
        }

        public static void BuildDependencies()
        {
            AppSettings.UseFakes = false;

            // Билдим зависимости нашего локатора
            Locator.Instance.Build();
        }

        /// <summary>
        /// Инициализация навигации
        /// </summary>
        private Task InitNavigation()
        {
            var navigationService = Locator.Instance.Resolve<INavigationService>();
            return navigationService.NavigateToAsync<ExtendedSplashViewModel>();
        }

        protected override void OnStart()
        {
            // Стартуем App Center
            AppCenter.Start(
                $"uwp={AppSettings.AppCenterAnalyticsWindows};" +
                $"android={AppSettings.AppCenterAnalyticsAndroid}",
                typeof(Analytics), typeof(Crashes), typeof(Distribute));
        }

        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}
