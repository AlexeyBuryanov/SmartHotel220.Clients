using SmartHotel220.Clients.Core.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// Тип уведомления к заголовку
    /// </summary>
    public class NotificationTypeToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is NotificationType notificationType)
            {
                switch(notificationType)
                {
                    case NotificationType.BeGreen:
                        return "Чистота";
                    case NotificationType.Hotel:
                        return "Отель";
                    case NotificationType.Room:
                        return "Номер";
                    case NotificationType.Other:
                        return "Другое";
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
