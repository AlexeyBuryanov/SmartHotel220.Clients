using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SmartHotel220.Clients.UWP.Extensions
{
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Найти дочерние визуальные элементы
        /// </summary>
        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                // Проходим по дочерним элементам объекта
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    // Получаем элемент
                    var child = VisualTreeHelper.GetChild(depObj, i);
                    // Если соответствует заданному типу - возвращаем
                    if (child != null && child is T variable)
                    {
                        yield return variable;
                    }

                    // Рекурсия
                    foreach (var childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
