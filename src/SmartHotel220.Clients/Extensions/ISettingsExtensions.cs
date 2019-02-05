using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Plugin.Settings.Abstractions;
using System;

namespace SmartHotel220.Clients.Core.Extensions
{
    /// <summary>
    /// Расширение для настроек ISettings
    /// </summary>
    public static class ISettingsExtensions
    {
        /// <summary>
        /// Получить значения или по умолчанию, если ничего не нашло
        /// </summary>
        public static T GetValueOrDefault<T>(this ISettings settings, string key, T @default) where T : class
        {
            var serialized = settings.GetValueOrDefault(key, string.Empty);
            var result = @default;

            try
            {
                var serializeSettings = GetSerializerSettings();
                result = JsonConvert.DeserializeObject<T>(serialized);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при дессериализации значений настроек: {ex}");
            }

            return result;
        }


        /// <summary>
        /// Добавить или обновить значение
        /// </summary>
        public static bool AddOrUpdateValue<T>(this ISettings settings, string key, T obj) where T : class
        {
            try
            {
                var serializeSettings = GetSerializerSettings();
                var serialized = JsonConvert.SerializeObject(obj, serializeSettings);
                
                return settings.AddOrUpdateValue(key, serialized);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сериализации значений настроек: {ex}");
            }

            return false;
        }

        /// <summary>
        /// Получить сериализованные настройки
        /// </summary>
        /// <returns></returns>
        private static JsonSerializerSettings GetSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
    }
}
