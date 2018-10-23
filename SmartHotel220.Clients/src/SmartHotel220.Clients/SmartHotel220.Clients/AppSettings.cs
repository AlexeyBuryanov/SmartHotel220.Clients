using Plugin.Settings;
using Plugin.Settings.Abstractions;
using SmartHotel220.Clients.Core.Extensions;
using SmartHotel220.Clients.Core.Models;

namespace SmartHotel220.Clients.Core
{
    public static class AppSettings
    {
        // AppCenter
        private const string DefaultAppCenterAndroid = "85527c99-5012-409b-b530-d59be7735cde";
        private const string DefaultAppCenterUWP = "4053c78f-7fdd-446b-9c84-6c48ccde367b";

        // Конечные точки
        private const string DefaultBookingEndpoint = "https://smarthotel220-apis.westeurope.cloudapp.azure.com/bookings-api/";
        private const string DefaultHotelsEndpoint = "https://smarthotel220-apis.westeurope.cloudapp.azure.com/hotels-api/";
        private const string DefaultSuggestionsEndpoint = "https://smarthotel220-apis.westeurope.cloudapp.azure.com/suggestions-api/";
        private const string DefaultNotificationsEndpoint = "https://smarthotel220-apis.westeurope.cloudapp.azure.com/notifications-api/";
        private const string DefaultSettingsFileUrl = "https://smarthotel220-apis.westeurope.cloudapp.azure.com/configuration-api/cfg/main/";
        private const string DefaultImagesBaseUri = "https://sh220imgs.blob.core.windows.net/";

        // Карты 
        private const string DefaultBingMapsApiKey = "mxkp3S6jkStPKhucwLce~l2wMIFb_beAr9vOTSOv88A~Av9hR9zx1OGnDVbj5uSSc2HYlimWkjuAzvDxsHWYcj-C2-Me0cRQETl4FJ24kLep";
        public const string DefaultFallbackMapsLocation = "50.4501,30.5234";
		
        // Боты
        private const string DefaultSkypeBotId = "smarthotel220";
        private const string DefaultFacebookBotId = "smarthotel220";
        private const string DefaultTelegramBotId = "alexeyburyanov_bot";

        // B2c
        public const string B2cAuthority = "https://login.microsoftonline.com/";
        public const string DefaultB2cPolicy = "B2C_1_SignUpInPolicy";
        public const string DefaultB2cClientId = "85f57866-2a32-4b87-bf8e-d30636c97916";
        public const string DefaultB2cTenant = "smarthotel220.onmicrosoft.com";

        // Бронирование 
        private const bool DefaultHasBooking = false;
        
        // Фейки
        private const bool DefaultUseFakes = false;

        private static ISettings Settings => CrossSettings.Current;

        // Azure B2C
        public static string B2cClientId
        {
            get => Settings.GetValueOrDefault(nameof(B2cClientId), DefaultB2cClientId);

            set => Settings.AddOrUpdateValue(nameof(B2cClientId), value);
        }

        public static string B2cTenant
        {
            get => Settings.GetValueOrDefault(nameof(B2cTenant), DefaultB2cTenant);

            set => Settings.AddOrUpdateValue(nameof(B2cTenant), value);
        }

        public static string B2cPolicy
        {
            get => Settings.GetValueOrDefault(nameof(B2cPolicy), DefaultB2cPolicy);

            set => Settings.AddOrUpdateValue(nameof(B2cPolicy), value);
        }
        

        // Конечные точки API
        public static string BookingEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(BookingEndpoint), DefaultBookingEndpoint);

            set => Settings.AddOrUpdateValue(nameof(BookingEndpoint), value);
        }

        public static string HotelsEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(HotelsEndpoint), DefaultHotelsEndpoint);

            set => Settings.AddOrUpdateValue(nameof(HotelsEndpoint), value);
        }

        public static string SuggestionsEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(SuggestionsEndpoint), DefaultSuggestionsEndpoint);

            set => Settings.AddOrUpdateValue(nameof(SuggestionsEndpoint), value);
        }

        public static string NotificationsEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(NotificationsEndpoint), DefaultNotificationsEndpoint);

            set => Settings.AddOrUpdateValue(nameof(NotificationsEndpoint), value);
        }

        public static string ImagesBaseUri
        {
            get => Settings.GetValueOrDefault(nameof(ImagesBaseUri), DefaultImagesBaseUri);

            set => Settings.AddOrUpdateValue(nameof(ImagesBaseUri), value);
        }

        public static string SkypeBotId
        {
            get => Settings.GetValueOrDefault(nameof(SkypeBotId), DefaultSkypeBotId);

            set => Settings.AddOrUpdateValue(nameof(SkypeBotId), value);
        }

        public static string FacebookBotId
        {
            get => Settings.GetValueOrDefault(nameof(FacebookBotId), DefaultFacebookBotId);

            set => Settings.AddOrUpdateValue(nameof(FacebookBotId), value);
        }

        public static string TelegramBotId 
        {
            get => Settings.GetValueOrDefault(nameof(TelegramBotId), DefaultTelegramBotId);

            set => Settings.AddOrUpdateValue(nameof(TelegramBotId), value);
        }

        // Другие настройки

        public static string BingMapsApiKey
        {
            get => Settings.GetValueOrDefault(nameof(BingMapsApiKey), DefaultBingMapsApiKey);

            set => Settings.AddOrUpdateValue(nameof(BingMapsApiKey), value);
        }

        public static string SettingsFileUrl
        {
            get => Settings.GetValueOrDefault(nameof(SettingsFileUrl), DefaultSettingsFileUrl);

            set => Settings.AddOrUpdateValue(nameof(SettingsFileUrl), value);
        }

        public static string FallbackMapsLocation
        {
            get => Settings.GetValueOrDefault(nameof(FallbackMapsLocation), DefaultFallbackMapsLocation);

            set => Settings.AddOrUpdateValue(nameof(FallbackMapsLocation), value);
        }

        public static User User
        {
            get => Settings.GetValueOrDefault(nameof(User), default(User));

            set => Settings.AddOrUpdateValue(nameof(User), value);
        }

        public static string AppCenterAnalyticsAndroid
        {
            get => Settings.GetValueOrDefault(nameof(AppCenterAnalyticsAndroid), DefaultAppCenterAndroid);

            set => Settings.AddOrUpdateValue(nameof(AppCenterAnalyticsAndroid), value);
        }

        public static string AppCenterAnalyticsWindows
        {
            get => Settings.GetValueOrDefault(nameof(AppCenterAnalyticsWindows), DefaultAppCenterUWP);

            set => Settings.AddOrUpdateValue(nameof(AppCenterAnalyticsWindows), value);
        }

        public static bool UseFakes
        {
            get => Settings.GetValueOrDefault(nameof(UseFakes), DefaultUseFakes);

            set => Settings.AddOrUpdateValue(nameof(UseFakes), value);
        }

        public static bool HasBooking
        {
            get => Settings.GetValueOrDefault(nameof(HasBooking), DefaultHasBooking);

            set => Settings.AddOrUpdateValue(nameof(HasBooking), value);
        }

        public static void RemoveUserData()
        {
            Settings.Remove(nameof(User));
        }
    }
}