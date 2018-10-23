using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Settings
{
    /// <summary>
    /// Описывает службу настроек
    /// </summary>
    public interface ISettingsService<TRemoteSettingsModel> : IBaseSettingsLoader<TRemoteSettingsModel> where TRemoteSettingsModel : class
    {
        /// <summary>
        /// URL удалённого файла
        /// </summary>
        string RemoteFileUrl { get; set; }

        /// <summary>
        /// Загрузить настройки
        /// </summary>
        Task<TRemoteSettingsModel> LoadSettingsAsync();

        /// <summary>
        /// Сохранить настройки
        /// </summary>
        Task PersistRemoteSettingsAsync(TRemoteSettingsModel remote);
    }
}
