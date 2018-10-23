using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    // Календарь. Дни недели
	public partial class Calendar
	{
	    private readonly List<Label> _dayLabels;

		#region Цвет текста

		public static readonly BindableProperty WeekdaysTextColorProperty =
			BindableProperty.Create(nameof(WeekdaysTextColor), typeof(Color), typeof(Calendar), Color.FromHex("#aaaaaa"),
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeWeekdaysTextColor((Color)newValue, (Color)oldValue));

		protected void ChangeWeekdaysTextColor(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_dayLabels.ForEach(l => l.TextColor = newValue);
		}

        /// <summary>
        /// Возвращает или задает цвет текста меток дней недели.
        /// </summary>
        public Color WeekdaysTextColor
		{
			get => (Color)GetValue(WeekdaysTextColorProperty);
            set => SetValue(WeekdaysTextColorProperty, value);
        }

		#endregion

		#region Фоновый цвет

		public static readonly BindableProperty WeekdaysBackgroundColorProperty =
			BindableProperty.Create(nameof(WeekdaysBackgroundColor), typeof(Color), typeof(Calendar), Color.Transparent,
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeWeekdaysBackgroundColor((Color)newValue, (Color)oldValue));

		protected void ChangeWeekdaysBackgroundColor(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_dayLabels.ForEach(l => l.BackgroundColor = newValue);
		}

        /// <summary>
        /// Возвращает или задает цвет фона меток дней недели.
        /// </summary>
        public Color WeekdaysBackgroundColor
		{
			get => (Color)GetValue(WeekdaysBackgroundColorProperty);
            set => SetValue(WeekdaysBackgroundColorProperty, value);
        }

		#endregion

		#region Размер шрифта

		public static readonly BindableProperty WeekdaysFontSizeProperty =
			BindableProperty.Create(nameof(WeekdaysFontSize), typeof(double), typeof(Calendar), 18.0,
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeWeekdaysFontSize((double)newValue, (double)oldValue));

		protected void ChangeWeekdaysFontSize(double newValue, double oldValue)
		{
			if (Math.Abs(newValue - oldValue) < 0.01) return;
			_dayLabels.ForEach(l => l.FontSize = newValue);
		}

        /// <summary>
        /// Возвращает или задает размер шрифта меток дня недели.
        /// </summary>
        public double WeekdaysFontSize
		{
			get => (double)GetValue(WeekdaysFontSizeProperty);
            set => SetValue(WeekdaysFontSizeProperty, value);
        }

		#endregion

		#region Семейство шрифта

		public static readonly BindableProperty WeekdaysFontFamilyProperty =
            BindableProperty.Create(nameof(WeekdaysFontFamily), typeof(string), typeof(Calendar), default(string),							
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeWeekdaysFontFamily((string)newValue, (string)oldValue));

		protected void ChangeWeekdaysFontFamily(string newValue, string oldValue)
		{
			if (newValue == oldValue) return;
			_dayLabels.ForEach(l => l.FontFamily = newValue);
		}

        /// <summary>
        /// Получает или задает семейство шрифтов меток дней недели.
        /// </summary>
        public string WeekdaysFontFamily
		{
			get => GetValue(WeekdaysFontFamilyProperty) as string;
            set => SetValue(WeekdaysFontFamilyProperty, value);
        }

		#endregion

		#region Формат

		public static readonly BindableProperty WeekdaysFormatProperty =
			BindableProperty.Create(nameof(WeekdaysFormat), typeof(string), typeof(Calendar), "ddd",								
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeWeekdays());

        /// <summary>
        /// Возвращает или задает формат даты меток дня недели.
        /// </summary>
        public string WeekdaysFormat
		{
			get => GetValue(WeekdaysFormatProperty) as string;
            set => SetValue(WeekdaysFormatProperty, value);
        }
		#endregion

		#region Атрибуты шрифта

		public static readonly BindableProperty WeekdaysFontAttributesProperty =
			BindableProperty.Create(nameof(WeekdaysFontAttributes), typeof(FontAttributes), typeof(Calendar), FontAttributes.None,		
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeWeekdaysFontAttributes((FontAttributes)newValue, (FontAttributes)oldValue));

		protected void ChangeWeekdaysFontAttributes(FontAttributes newValue, FontAttributes oldValue)
		{
			if (newValue == oldValue) return;
			_dayLabels.ForEach(l => l.FontAttributes = newValue);
		}

        /// <summary>
        /// Получает или задает атрибуты шрифта меток дня недели.
        /// </summary>
        public FontAttributes WeekdaysFontAttributes
		{
			get => (FontAttributes)GetValue(WeekdaysFontAttributesProperty);
            set => SetValue(WeekdaysFontAttributesProperty, value);
        }

		#endregion

		#region Показ дней недели

		public static readonly BindableProperty WeekdaysShowProperty =
			BindableProperty.Create(nameof(WeekdaysShow), typeof(bool), typeof(Calendar), true,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ShowHideElements());

        /// <summary>
        /// Возвращает или задает, показывать ли метки дней недели.
        /// </summary>
        public bool WeekdaysShow
		{
			get => (bool)GetValue(WeekdaysShowProperty);
            set => SetValue(WeekdaysShowProperty, value);
        }

		#endregion

        /// <summary>
        /// Изменить дни недели
        /// </summary>
		protected void ChangeWeekdays()
		{
			if (!WeekdaysShow) return;

			var start = CalendarStartDate(StartDate);
            
			foreach (var t in _dayLabels)
			{
			    var day = start.ToString(WeekdaysFormat);
			    var showDay = char.ToUpper(day.First()) + day.Substring(1).ToLower();
			    t.Text = showDay;
			    start = start.AddDays(1);
			}
		}
	}
}