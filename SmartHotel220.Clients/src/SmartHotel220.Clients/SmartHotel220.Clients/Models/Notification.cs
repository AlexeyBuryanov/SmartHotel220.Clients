using System;

namespace SmartHotel220.Clients.Core.Models
{
    /// <summary>
    /// Уведомление
    /// </summary>
    public class Notification
    {
        public int Seq { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Время
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public NotificationType Type { get; set; }
    }
}