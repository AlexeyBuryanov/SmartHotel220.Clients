using SmartHotel220.Clients.Core.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// Конвертер типа элемента меню
    /// </summary>
    public class MenuItemTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var menuItemType = (MenuItemType)value;

            var platform = Device.RuntimePlatform == Device.UWP;

            switch (menuItemType)
            {
                case MenuItemType.Home:
                    return platform ? "Assets/ic_home.png" : "ic_home.png";
                case MenuItemType.BookRoom:
                    return platform ? "Assets/ic_bed.png" : "ic_bed.png";
                case MenuItemType.MyRoom:
                    return platform ? "Assets/ic_key.png" : "ic_key.png";
                case MenuItemType.Suggestions:
                    return platform ? "Assets/ic_beach.png" : "ic_beach.png";
                case MenuItemType.Concierge:
                    return platform ? "Assets/ic_bot.png" : "ic_bot.png";
                case MenuItemType.Logout:
                    return platform ? "Assets/ic_logout.png" : "ic_logout.png";
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}