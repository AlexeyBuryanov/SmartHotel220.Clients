using SmartHotel220.Clients.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Notification
{
    public class FakeNotificationService : INotificationService
    {
        public Task<IEnumerable<Models.Notification>> GetNotificationsAsync(int seq, string token)
        {
            var data = new List<Models.Notification>
            {
                new Models.Notification { Text = "Службы по уборке закончили освежать номер.", Type = NotificationType.BeGreen },
                new Models.Notification { Text = "Ваше обращение о вызове сантехников было подтверждено в 5:30 утра.", Type = NotificationType.Room },
                new Models.Notification { Text = "Ваш конференц-зал готов по вашему онлайн-запросу.", Type = NotificationType.Hotel },
                new Models.Notification { Text = "Бар SmartHotel220 с 8 вечера имеет ограничение только для 3 гостей.", Type = NotificationType.Other }
            };

            return Task.FromResult(data.AsEnumerable());
        }

        public Task DeleteNotificationAsync(Models.Notification notification)
        {
            return Task.FromResult(false);
        }
    }
}