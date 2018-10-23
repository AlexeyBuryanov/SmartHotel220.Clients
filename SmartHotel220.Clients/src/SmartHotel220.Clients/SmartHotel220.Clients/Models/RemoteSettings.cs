using Newtonsoft.Json;

namespace SmartHotel220.Clients.Core.Models
{
    /// <summary>
    /// Удалённый настройки
    /// </summary>
    public class RemoteSettings
    {
        public RemoteSettings()
        {
            Urls = new EndpointsData();
            Tokens = new TokensData();
            B2c = new B2cData();
            Analytics = new AnalyticsData();
            Bot = new BotData();
            Others = new OthersData();
        }

        public EndpointsData Urls { get; set; }

        public TokensData Tokens { get; set; }

        public B2cData B2c { get; set; }

        public AnalyticsData Analytics { get; set; }

        public BotData Bot { get; set; }

        public OthersData Others { get; set; }

        /// <summary>
        /// Ссылки на API
        /// </summary>
        public class EndpointsData
        {
            /// <summary>
            /// Отели API
            /// </summary>
            public string Hotels { get; set; }

            /// <summary>
            /// Бронирования API
            /// </summary>
            public string Bookings { get; set; }

            /// <summary>
            /// Предложения API
            /// </summary>
            public string Suggestions { get; set; }

            /// <summary>
            /// Уведомления API
            /// </summary>
            public string Notifications { get; set; }

            /// <summary>
            /// Хранилище с картинками 
            /// </summary>
            [JsonProperty("images_base")]
            public string ImagesBaseUri { get; set; }
        }

        /// <summary>
        /// Различные токены для доступа к стороннему API
        /// </summary>
        public class TokensData
        {
            public string Bingmaps { get; set; }
        }

        /// <summary>
        /// Azure Active Directory B2C
        /// </summary>
        public class B2cData
        {
            /// <summary>
            /// ID клиента
            /// </summary>
            public string Client { get; set; }

            /// <summary>
            /// Домен
            /// </summary>
            public string Tenant { get; set; }

            /// <summary>
            /// Политика входа/регистрации
            /// </summary>
            public string Policy { get; set; }
        }

        /// <summary>
        /// Аналитика приложения с помощью AppCenter
        /// </summary>
        public class AnalyticsData
        {
            public string Android { get; set; }

            public string Uwp { get; set; }
        }

        /// <summary>
        /// Остальные данные
        /// </summary>
        public class OthersData
        {
            public string FallbackMapsLocation { get; set; }
        }

        /// <summary>
        /// Данные для ботов
        /// </summary>
        public class BotData
        {
            [JsonProperty(PropertyName = "FacebookBotId")]
            public string FacebookId { get; set; }

            [JsonProperty(PropertyName = "id")]
            public string SkypeId { get; set; }
        }
    }
}
