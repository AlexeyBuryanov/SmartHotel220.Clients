using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Android.Views;
using CarouselView.FormsPlugin.Android;
using Microsoft.Identity.Client;
using Plugin.Permissions;
using SmartHotel220.Clients.Core;
using SmartHotel220.Clients.Core.Helpers;
using SmartHotel220.Clients.Core.Services.Authentication;
using SmartHotel220.Clients.Core.ViewModels.Base;
using SmartHotel220.Clients.Droid.Services.Authentication;
using SmartHotel220.Clients.Droid.Services.CardEmulation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace SmartHotel220.Clients.Droid
{
    [Activity(
        Label = "SmartHotel220", 
        Icon = "@drawable/icon", 
        Theme = "@style/MainTheme", 
        MainLauncher = false,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            // Ресурс для Tabbar
            TabLayoutResource = Resource.Layout.Tabbar;
            // Ресурс для Toolbar
            ToolbarResource = Resource.Layout.Toolbar;

            // Создание бандла
            base.OnCreate(bundle);

            // Инициализация форм
            Forms.Init(this, bundle);
            // Инициализация карусели
            CarouselViewRenderer.Init();
            // Инициализация пользовательских диалогов
            UserDialogs.Init(this);
            // Инициализация календаря
            Renderers.Calendar.Init();
            // Инициализация карт
            Xamarin.FormsMaps.Init(this, bundle);

            // Инициализация центра сообщений и подписок
            InitMessageCenterSubscriptions();

            // Регистрация зависимостей
            RegisterPlatformDependencies();
            // Загрузка приложения
            LoadApplication(new App());

            // Параметры текущей платформы
            App.AuthenticationClient.PlatformParameters = new PlatformParameters(this);

            // Статус-бар при запуске НЕ прозрачный
            MakeStatusBarTranslucent(false);
            // Инициализация службы NFC
            InitNfcService();
        }

        private void InitNfcService()
        {
            // Стартуем наш сервис эмулятор карты
            //StartService(new Intent(this, typeof(CardService)));
            // Отключаем NFC
            DisableNfcService();
            // Подписываемся на событие отправки NFC-токена
            MessagingCenter.Subscribe<string>(this, MessengerKeys.SendNFCToken, EnableNfcService);
        }

        /// <summary>
        /// При запросе разрешения
        /// </summary>
        /// <param name="requestCode">код запроса</param>
        /// <param name="permissions">разрешения</param>
        /// <param name="grantResults">результаты</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            // Запрашиваем нужные разрешения
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
        /// <summary>
        /// Вызывается, когда активность, которая запущена завершает работу, предоставляя
        /// нам код запроса с которого она запустилась 
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="resultCode"></param>
        /// <param name="data"></param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            // Для Identity.Client по правилам завершения работы
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }

        private void InitMessageCenterSubscriptions()
        {
            // Подписываемся на сообщения об изменении прозрачности статус-бару
            MessagingCenter.Instance.Subscribe<StatusBarHelper, bool>(this, StatusBarHelper.TranslucentStatusChangeMessage, OnTranslucentStatusRequest);
        }

        /// <summary>
        /// При запросе сделать статус-бар прозрачным
        /// </summary>
        private void OnTranslucentStatusRequest(StatusBarHelper helper, bool makeTranslucent)
        {
            MakeStatusBarTranslucent(makeTranslucent);
        }

        /// <summary>
        /// Сделать статус-бар прозрачным
        /// </summary>
        private void MakeStatusBarTranslucent(bool makeTranslucent)
        {
            // Если да
            if (makeTranslucent)
            {
                // Прозрачный цвет
                SetStatusBarColor(Android.Graphics.Color.Transparent);

                // Если SDK выше, чем Android 5.0
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    // Настраиваем UI с помощью двух флагов
                    Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.LayoutFullscreen | SystemUiFlags.LayoutStable);
                }
            }
            // Если нет
            else
            {
                using (var value = new TypedValue())
                {
                    // Запрашиваем тёмный цвет из темы и устанавливаем его статус-бару
                    if (Theme.ResolveAttribute(Resource.Attribute.colorPrimaryDark, value, true))
                    {
                        var color = new Android.Graphics.Color(value.Data);
                        SetStatusBarColor(color);
                    }
                }

                // Если SDK больше, чем Android 5.0
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    // Отображаем все UI
                    Window.DecorView.SystemUiVisibility = StatusBarVisibility.Visible;
                }
            }
        }

        private static void RegisterPlatformDependencies()
        {
            // Регистрируем службу куки
            Locator.Instance.Register<IBrowserCookiesService, BrowserCookiesService>();
        }

        /// <summary>
        /// Отключить NFC не убивая приложение
        /// </summary>
        private void DisableNfcService()
        {
            var pm = PackageManager;
            pm.SetComponentEnabledSetting(
                new ComponentName(this, CardService.ServiceName),
                ComponentEnabledState.Disabled,
                ComponentEnableOption.DontKillApp);
        }

        /// <summary>
        /// Включить NFC не убивая приложение
        /// </summary>
        private void EnableNfcService(string message = "")
        {
            var pm = PackageManager;
            pm.SetComponentEnabledSetting(
                new ComponentName(this, CardService.ServiceName),
                ComponentEnabledState.Enabled,
                ComponentEnableOption.DontKillApp);
        }
    }
}