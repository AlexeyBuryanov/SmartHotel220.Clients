namespace SmartHotel220.Clients.Core.Services.OpenUri
{
    /// <summary>
    /// Сервис для открытия по Uri
    /// </summary>
    public interface IOpenUriService
    {
        /// <summary>
        /// Открыть ури
        /// </summary>
        void OpenUri(string uri);

        /// <summary>
        /// Открыть фб бот
        /// </summary>
        void OpenFacebookBot(string botId);

        /// <summary>
        /// Открыть скайп бот
        /// </summary>
        void OpenSkypeBot(string botId);

        /// <summary>
        /// Открыть телеграм бот
        /// </summary>
        void OpenTelegramBot(string botId);
    }
}