using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Behaviors
{
    /// <summary>
    /// Поведение для команды "по тапу на элементе ListView"
    /// </summary>
    public sealed class ItemTappedCommandListView
    {
        /// <summary>
        /// Св-во привязки для данной команды
        /// </summary>
        public static readonly BindableProperty ItemTappedCommandProperty =
            BindableProperty.CreateAttached("ItemTappedCommand", typeof(ICommand), typeof(ItemTappedCommandListView),
                default(ICommand), BindingMode.OneWay, null, PropertyChanged);

        /// <summary>
        /// При изменении св-ва
        /// </summary>
        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Если bindable это ListView, то назначаем событие по тапу
            if (bindable is ListView listView)
            {
                listView.ItemTapped -= ListViewOnItemTapped;
                listView.ItemTapped += ListViewOnItemTapped;
            }
        }

        /// <summary>
        /// Событие при тапе по элементу ListView
        /// </summary>
        private static void ListViewOnItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Если отправитель ListView и ListView включено, и не обновляется
            if (sender is ListView list && list.IsEnabled && !list.IsRefreshing)
            {
                // Сбрасываем выбранный элемент
                list.SelectedItem = null;
                // Получаем команду
                var command = GetItemTappedCommand(list);
                // Если команда получена и может выполняться - выполняем
                if (command != null && command.CanExecute(e.Item))
                {
                    command.Execute(e.Item);
                }
            }
        }

        public static ICommand GetItemTappedCommand(BindableObject bindableObject)
        {
            return (ICommand)bindableObject.GetValue(ItemTappedCommandProperty);
        }

        public static void SetItemTappedCommand(BindableObject bindableObject, object value)
        {
            bindableObject.SetValue(ItemTappedCommandProperty, value);
        }
    }
}