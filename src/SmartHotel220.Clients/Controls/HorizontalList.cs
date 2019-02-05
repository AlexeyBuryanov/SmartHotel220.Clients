using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    /// <inheritdoc />
    /// <summary>
    /// Горизонтальный список
    /// </summary>
    public class HorizontalList : Grid
    {
        /// <summary>
        /// Входящая команда выбора
        /// </summary>
        private ICommand _innerSelectedCommand;
        /// <summary>
        /// Скролл
        /// </summary>
        private readonly ScrollView _scrollView;
        /// <summary>
        /// Контейнер
        /// </summary>
        private readonly StackLayout _itemsStackLayout;

        /// <summary>
        /// Событие "выбранный элемент изменился"
        /// </summary>
        public event EventHandler SelectedItemChanged;

        /// <summary>
        /// Ориентация
        /// </summary>
        public StackOrientation ListOrientation { get; set; }

        /// <summary>
        /// Отступ между элементами
        /// </summary>
        public double Spacing { get; set; }

        public static readonly BindableProperty SelectedCommandProperty =
            BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(HorizontalList), null);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(HorizontalList), default(IEnumerable<object>), BindingMode.TwoWay, 
                propertyChanged: ItemsSourceChanged);

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem", typeof(object), typeof(HorizontalList), null, BindingMode.TwoWay, 
                propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(HorizontalList), default(DataTemplate));

        public ICommand SelectedCommand
        {
            get => (ICommand)GetValue(SelectedCommandProperty);
            set => SetValue(SelectedCommandProperty, value);
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (HorizontalList)bindable;
            itemsLayout.SetItems();
        }

        public HorizontalList()
        {
            // Создаём скролл
            _scrollView = new ScrollView();

            // Создаём контейнер, который представляет контент
            _itemsStackLayout = new StackLayout
            {
                BackgroundColor = BackgroundColor,
                Padding = Padding,
                Spacing = Spacing,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            _scrollView.BackgroundColor = BackgroundColor;
            _scrollView.Content = _itemsStackLayout;

            // Добавляем скролл в сетку
            Children.Add(_scrollView);
        }

        /// <summary>
        /// Установить элементы
        /// </summary>
        protected virtual void SetItems()
        {
            _itemsStackLayout.Children.Clear();
            _itemsStackLayout.Spacing = Spacing;

            _innerSelectedCommand = new Command<View>(view =>
            {
                SelectedItem = view.BindingContext;
                SelectedItem = null; // Разрешить айтему второй выбор
            });

            _itemsStackLayout.Orientation = ListOrientation;
            _scrollView.Orientation = ListOrientation == StackOrientation.Horizontal
                ? ScrollOrientation.Horizontal
                : ScrollOrientation.Vertical;

            if (ItemsSource == null)
            {
                return;
            }

            // Проходим по источнику и добавляем элементы в наш контейнер
            foreach (var item in ItemsSource)
            {
                _itemsStackLayout.Children.Add(GetItemView(item));
            }

            _itemsStackLayout.BackgroundColor = BackgroundColor;
            SelectedItem = null;
        }

        /// <summary>
        /// Представление одного элемента
        /// </summary>
        protected virtual View GetItemView(object item)
        {
            var content = ItemTemplate.CreateContent();

            if (!(content is View view))
            {
                return null;
            }

            view.BindingContext = item;

            // Создаём команду
            var gesture = new TapGestureRecognizer
            {
                Command = _innerSelectedCommand,
                CommandParameter = view
            };

            // Добавляем новый жест
            AddGesture(view, gesture);

            return view;
        }

        /// <summary>
        /// Добавить жест
        /// </summary>
        /// <param name="view">Представление</param>
        /// <param name="gesture">Жест</param>
        private void AddGesture(View view, IGestureRecognizer gesture)
        {
            // Добавляем
            view.GestureRecognizers.Add(gesture);

            // Если представление это не Layout<View> - выход
            if (!(view is Layout<View> layout))
            {
                return;
            }

            // Проходим по разметке
            foreach (var child in layout.Children)
            {
                // Добавляем жест (рекурсия)
                AddGesture(child, gesture);
            }
        }

        /// <summary>
        /// Когда выбранный элемент изменяется
        /// </summary>
        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsView = (HorizontalList)bindable;
            if (newValue == oldValue && newValue != null)
            {
                return;
            }

            itemsView.SelectedItemChanged?.Invoke(itemsView, EventArgs.Empty);

            if (itemsView.SelectedCommand?.CanExecute(newValue) ?? false)
            {
                itemsView.SelectedCommand?.Execute(newValue);
            }
        }
    }
}