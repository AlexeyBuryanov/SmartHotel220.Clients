using FFImageLoading.Forms.WinUWP;
using SmartHotel220.Clients.Core;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace SmartHotel220.Clients.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Инициализация FFImageLoading
            CachedImageRenderer.Init();
            // Инициализация календаря
            Renderers.Calendar.Init();
            // Инициализация карт
            Xamarin.FormsMaps.Init(AppSettings.BingMapsApiKey);
            // Загрузка приложения
            LoadApplication(new Clients.App());
            // Нативная кастомизация
            NativeCustomize();
        }

        private void NativeCustomize()
        {
            // Минимальный размер окна
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(500, 500));

            // ПК кастомизация
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.BackgroundColor = (Color)Application.Current.Resources["NativeAccentColor"];
                    titleBar.ButtonBackgroundColor = (Color)Application.Current.Resources["NativeAccentColor"];
                }
            }

            // Мобильная кастомизация
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = (Color)Application.Current.Resources["NativeAccentColor"];
                }
            }

            // Запуск в режиме окна
            var currentView = ApplicationView.GetForCurrentView();
            if (currentView.IsFullScreenMode)
            {
                currentView.ExitFullScreenMode();
            }
        }
    }
}