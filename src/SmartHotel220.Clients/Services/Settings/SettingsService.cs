using SmartHotel220.Clients.Core.Models;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Settings
{
    /// <summary>
    /// Служба настроек
    /// </summary>
    public class SettingsService : BaseSettingsLoader<RemoteSettings>, ISettingsService<RemoteSettings>
    {
        public string RemoteFileUrl
        {
            get => AppSettings.SettingsFileUrl;
            set => AppSettings.SettingsFileUrl = value;
        }

        /// <summary>
        /// Загрузка настроек
        /// </summary>
        public Task<RemoteSettings> LoadSettingsAsync()
        {
            // Формируем настройки
            var settings = new RemoteSettings
            {
                Urls =
                {
                    Bookings = AppSettings.BookingEndpoint,
                    Hotels = AppSettings.HotelsEndpoint,
                    Suggestions = AppSettings.SuggestionsEndpoint,
                    Notifications = AppSettings.NotificationsEndpoint,
                    ImagesBaseUri = AppSettings.ImagesBaseUri
                },
                Tokens = { Bingmaps = AppSettings.BingMapsApiKey },
                B2c =
                {
                    Client = AppSettings.B2cClientId,
                    Tenant = AppSettings.B2cTenant,
                    Policy = AppSettings.B2cPolicy
                },
                Analytics =
                {
                    Android = AppSettings.AppCenterAnalyticsAndroid,
                    Uwp = AppSettings.AppCenterAnalyticsWindows
                },
                Others = { FallbackMapsLocation = AppSettings.FallbackMapsLocation },
                Bot =
                {
                    FacebookId = AppSettings.FacebookBotId,
                    SkypeId = AppSettings.SkypeBotId
                }
            };

            return Task.FromResult(settings);
        }

        /// <summary>
        /// Сохранить удалённые настройки
        /// </summary>
        public Task PersistRemoteSettingsAsync(RemoteSettings remote)
        {
            AppSettings.BookingEndpoint = remote.Urls.Bookings;
            AppSettings.HotelsEndpoint = remote.Urls.Hotels;
            AppSettings.SuggestionsEndpoint = remote.Urls.Suggestions;
            AppSettings.NotificationsEndpoint = remote.Urls.Notifications;
            AppSettings.ImagesBaseUri = remote.Urls.ImagesBaseUri;
            AppSettings.BingMapsApiKey = remote.Tokens.Bingmaps;
            AppSettings.B2cClientId = remote.B2c.Client;
            AppSettings.B2cTenant = remote.B2c.Tenant;
            AppSettings.B2cPolicy = remote.B2c.Policy;
            AppSettings.AppCenterAnalyticsAndroid = remote.Analytics.Android;
            AppSettings.AppCenterAnalyticsWindows = remote.Analytics.Uwp;
            AppSettings.FallbackMapsLocation = remote.Others.FallbackMapsLocation;
            AppSettings.FacebookBotId = remote.Bot.FacebookId;
            AppSettings.SkypeBotId = remote.Bot.SkypeId;

            return Task.FromResult(false);
        }
    }
}
