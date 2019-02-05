using System;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    /// <inheritdoc />
    /// <summary>
    /// Кнопка календаря
    /// </summary>
    public class CalendarButton : Button
    {
        public CalendarButton()
        {
            HeightRequest = 48;
            WidthRequest = 48;
        }

		public static readonly BindableProperty DateProperty =
            BindableProperty.Create(nameof(Date), typeof(DateTime?), typeof(CalendarButton), null);

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime? Date
        {
            get => (DateTime?)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(CalendarButton), false);

        /// <summary>
        /// Выбрана ли
        /// </summary>
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly BindableProperty IsOutOfMonthProperty =
            BindableProperty.Create(nameof(IsOutOfMonth), typeof(bool), typeof(CalendarButton), false);

        /// <summary>
        /// За пределами месяца (true, false)
        /// </summary>
        public bool IsOutOfMonth
        {
            get => (bool)GetValue(IsOutOfMonthProperty);
            set => SetValue(IsOutOfMonthProperty, value);
        }

        public static readonly BindableProperty TextWithoutMeasureProperty =
            BindableProperty.Create(nameof(TextWithoutMeasure), typeof(string), typeof(Button), null);

        /// <summary>
        /// Текст
        /// </summary>
        public string TextWithoutMeasure
        {
            get
            {
                var text = (string)GetValue(TextWithoutMeasureProperty);
                return string.IsNullOrEmpty(text) ? Text : text;
            }
            set => SetValue(TextWithoutMeasureProperty, value);
        }

		public static readonly BindableProperty BackgroundPatternProperty =
			BindableProperty.Create(nameof(BackgroundPattern), typeof(BackgroundPattern), typeof(Button), null);

        /// <summary>
        /// Фоновый паттерн
        /// </summary>
		public BackgroundPattern BackgroundPattern
		{
			get => (BackgroundPattern)GetValue(BackgroundPatternProperty);
		    set => SetValue(BackgroundPatternProperty, value);
		}

		public static readonly BindableProperty BackgroundImageProperty =
			BindableProperty.Create(nameof(BackgroundImage), typeof(FileImageSource), typeof(Button), null);

        /// <summary>
        /// Фоновое изображение
        /// </summary>
		public FileImageSource BackgroundImage
		{
			get => (FileImageSource)GetValue(BackgroundImageProperty);
		    set => SetValue(BackgroundImageProperty, value);
		}
    }
}