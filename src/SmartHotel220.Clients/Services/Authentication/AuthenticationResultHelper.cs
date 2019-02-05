using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace SmartHotel220.Clients.Core.Services.Authentication
{
    /// <summary>
    /// Помошник аутентификации
    /// </summary>
    public static class AuthenticationResultHelper
    {
        /// <summary>
        /// Получить пользователя из результата аутентификации
        /// </summary>
        /// <param name="ar">Результат аутентификации</param>
        public static Models.User GetUserFromResult(AuthenticationResult ar)
        {
            var data = ParseIdToken(ar.IdToken);

            var user = new Models.User
            {
                Id = ar.User.UniqueId,
                Token = ar.Token,
                Email = GetTokenValue(data, "emails"),
                Name = GetTokenValue(data, "given_name"),
                LastName = GetTokenValue(data, "family_name")
            };

            return user;
        }

        /// <summary>
        /// Разбор ID токена аутентификации
        /// </summary>
        private static JObject ParseIdToken(string idToken)
        {
            // Получаем кусочек с фактической информацией о пользователе
            idToken = idToken.Split('.')[1];
            idToken = Base64UrlDecode(idToken);

            return JObject.Parse(idToken);
        }

        /// <summary>
        /// Для декодирования строки base64
        /// </summary>
        private static string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);

            return decoded;
        }

        private static string GetTokenValue(JObject data, string key)
        {
            var value = string.Empty;

            try
            {
                var token = data[key];

                value = token.HasValues
                    ? token.First.ToString()
                    : token.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при получении токена данных B2C: {ex}");
            }

            return value;
        }
    }
}