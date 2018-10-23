using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Settings
{
    /// <summary>
    /// Описывает базовый класс настроек
    /// </summary>
    public interface IBaseSettingsLoader<TRemoteSettingsModel> where TRemoteSettingsModel : class
    {
        /// <summary>
        /// Загрузка удалённых настроек
        /// </summary>
        Task<TRemoteSettingsModel> LoadRemoteSettingsAsync(string fileUrl);
    }

    /// <summary>
    /// Загрузчик настроек
    /// </summary>
    /// <typeparam name="TRemoteSettingsModel">Тип настроек</typeparam>
    public abstract class BaseSettingsLoader<TRemoteSettingsModel> : IBaseSettingsLoader<TRemoteSettingsModel>
        where TRemoteSettingsModel : class
    {
        private readonly JsonSerializerSettings _serializerSettings;

        protected BaseSettingsLoader()
        {
            // Сериалайзер
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
        }

        /// <summary>
        /// Загрузка удалённых настроек
        /// </summary>
        /// <param name="fileUrl">Файл</param>
        public async Task<TRemoteSettingsModel> LoadRemoteSettingsAsync(string fileUrl)
        {
            System.Diagnostics.Debug.WriteLine($"Загрузка удаленных настроек из {fileUrl}");

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, fileUrl))
            using (var response = await client.SendAsync(request))
            {
                System.Diagnostics.Debug.WriteLine($"Получен ответ удаленных настроек. Успешно? {response.IsSuccessStatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Загрузка удаленных настроек не удалась. Статус код: {response.StatusCode}, Причина: {response.ReasonPhrase}");
                }

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                {
                    var data = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<TRemoteSettingsModel>(data);
                }
            }
        }
    }
}
