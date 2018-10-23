namespace SmartHotel220.Clients.Core.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        public string Id { get; set; }

        /// <summary>
        /// Токен входа
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Эл. почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// gravatar url
        /// </summary>
        public string AvatarUrl { get; set; }

        public bool LoggedInWithMicrosoftAccount { get; set; }
    }
}
