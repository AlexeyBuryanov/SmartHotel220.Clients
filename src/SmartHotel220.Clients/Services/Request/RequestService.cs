using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Request
{
    /// <inheritdoc />
    /// <summary>
    /// Служба запросов
    /// </summary>
    public class RequestService : IRequestService
    {
        /// <summary>
        /// Сериалайзер настроек
        /// </summary>
        private readonly JsonSerializerSettings _serializerSettings;

        public RequestService()
        {
            // Сериалайзер
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="TResult">Тип результата</typeparam>
        /// <param name="token">Токен авторизации</param>
        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            // Создаём http-клиент
            var httpClient = CreateHttpClient(token);
            // Получаем ответ
            var response = await httpClient.GetAsync(uri);
            // Обрабатываем ответ
            await HandleResponse(response);
            // Сериализуем ответ в строку
            var serialized = await response.Content.ReadAsStringAsync();
            // Получаем результат путём десериализации json
            var result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }

        public Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "")
        {
            return PostAsync<TResult, TResult>(uri, data, token);
        }

        public async Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data, string token = "")
        {
            // Создаём http-клиент
            var httpClient = CreateHttpClient(token);
            // Сериализуем ответ в строку
            var serialized = await Task.Run(() => JsonConvert.SerializeObject(data, _serializerSettings));
            // Получаем ответ
            var response = await httpClient.PostAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"));
            // Обрабатываем ответ
            await HandleResponse(response);
            // Получаем данные
            var responseData = await response.Content.ReadAsStringAsync();

            // Возвращаем ожидающую задачу, которая занимается десериализацией полученных данных пришедших в ответе
            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData, _serializerSettings));
        }

        public Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "")
        {
            return PutAsync<TResult, TResult>(uri, data, token);
        }

        public async Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data, string token = "")
        {
            // Создаём http-клиент
            var httpClient = CreateHttpClient(token);
            // Сериализуем ответ в строку
            var serialized = await Task.Run(() => JsonConvert.SerializeObject(data, _serializerSettings));
            // Получаем ответ
            var response = await httpClient.PutAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"));
            // Обрабатываем ответ
            await HandleResponse(response);
            // Получаем данные
            var responseData = await response.Content.ReadAsStringAsync();

            // Возвращаем ожидающую задачу, которая занимается десериализацией полученных данных пришедших в ответе
            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseData, _serializerSettings));
        }

        /// <summary>
        /// Создать HttpClient на основе токена
        /// </summary>
        private HttpClient CreateHttpClient(string token = "")
        {
            // Создаём http-клиент
            var httpClient = new HttpClient();
            // Настраиваем заголовок на работу с json
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Если есть токен
            if (!string.IsNullOrEmpty(token))
            {
                // Добавляем авторизацию на основе Bearer токена
                httpClient.DefaultRequestHeaders.Authorization = IsEmail(token) 
                    ? new AuthenticationHeaderValue("Email", token) 
                    : new AuthenticationHeaderValue("Bearer", token);
            }

            return httpClient;
        }

        /// <summary>
        /// Проверка email
        /// </summary>
        private bool IsEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        /// <summary>
        /// Обработка ответа
        /// </summary>
        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(content);
                }

                throw new HttpRequestException(content);
            }
        }
    }
}