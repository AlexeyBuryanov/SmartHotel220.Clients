using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Notification
{
    /// <summary>
    /// Описывает службу уведомлений
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Получить уведомления
        /// </summary>
        Task<IEnumerable<Models.Notification>> GetNotificationsAsync(int seq, string token);

        /// <summary>
        /// Удалить уведомление
        /// </summary>
        Task DeleteNotificationAsync(Models.Notification notification);
    }
}