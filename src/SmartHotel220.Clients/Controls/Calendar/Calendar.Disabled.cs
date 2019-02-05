using System;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    // Отключенный календарь
	public partial class Calendar
	{
		#region Отключить все даты

		public static readonly BindableProperty DisableAllDatesProperty = 
		    BindableProperty.Create(nameof(DisableAllDates), typeof(bool), typeof(Calendar), false,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.RaiseSpecialDatesChanged());

        /// <summary>
        /// Получает или устанавливает, что все даты должны быть отключены по умолчанию или нет
        /// </summary>
        /// <value></value>
        public bool DisableAllDates
		{
			get => (bool)GetValue(DisableAllDatesProperty);
            set => SetValue(DisableAllDatesProperty, value);
        }

        #endregion

	    #region Отключить лимит дат до максимального минимального диапазона

        public static readonly BindableProperty DisableDatesLimitToMaxMinRangeProperty = 
            BindableProperty.Create(nameof(DisableDatesLimitToMaxMinRange), typeof(bool), typeof(Calendar), false,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.RaiseSpecialDatesChanged());

        /// <summary>
        /// Возвращает или устанавливает отключенные даты, ограничивающие прокрутку влево / вправо (по умолчанию - false)
        /// </summary>
        /// <value></value>
        public bool DisableDatesLimitToMaxMinRange
		{
			get => (bool)GetValue(DisableDatesLimitToMaxMinRangeProperty);
            set => SetValue(DisableDatesLimitToMaxMinRangeProperty, value);
        }

        #endregion

        #region Ширина отключенной границы (border'a)

        public static readonly BindableProperty DisabledBorderWidthProperty =
			BindableProperty.Create(nameof(DisabledBorderWidth), typeof(int), typeof(Calendar), 3,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDisabledBorderWidth((int)newValue, (int)oldValue));

		protected void ChangeDisabledBorderWidth(int newValue, int oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => !b.IsEnabled).ForEach(b => b.BorderWidth = newValue);
		}

        /// <summary>
        /// Получает или задает ширину границы отключенных дат.
        /// </summary>
        /// <value>Ширина отключенной границы.</value>
        public int DisabledBorderWidth
		{
			get => (int)GetValue(DisabledBorderWidthProperty);
            set => SetValue(DisabledBorderWidthProperty, value);
        }

		#endregion

		#region Отключенный цвет границы

		public static readonly BindableProperty DisabledBorderColorProperty =
			BindableProperty.Create(nameof(DisabledBorderColor), typeof(Color), typeof(Calendar), Color.FromHex("#cccccc"),
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDisabledBorderColor((Color)newValue, (Color)oldValue));

		protected void ChangeDisabledBorderColor(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => !b.IsEnabled).ForEach(b => b.BorderColor = newValue);
		}

        /// <summary>
        /// Получает или задает цвет границы отключенных дат.
        /// </summary>
        /// <value>Цвет отключенной границы.</value>
        public Color DisabledBorderColor
		{
			get => (Color)GetValue(DisabledBorderColorProperty);
            set => SetValue(DisabledBorderColorProperty, value);
        }

		#endregion

		#region Отключенный цвет фона

		public static readonly BindableProperty DisabledBackgroundColorProperty =
			BindableProperty.Create(nameof(DisabledBackgroundColor), typeof(Color), typeof(Calendar), Color.Gray,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDisabledBackgroundColor((Color)newValue, (Color)oldValue));

		protected void ChangeDisabledBackgroundColor(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => !b.IsEnabled).ForEach(b => b.BackgroundColor = newValue);
		}

        /// <summary>
        /// Возвращает или задает цвет фона для отключенных дат.
        /// </summary>
        /// <value>Цвет фона.</value>
        public Color DisabledBackgroundColor
		{
			get => (Color)GetValue(DisabledBackgroundColorProperty);
            set => SetValue(DisabledBackgroundColorProperty, value);
        }

		#endregion

		#region Отключенный цвет текста

		public static readonly BindableProperty DisabledTextColorProperty =
			BindableProperty.Create(nameof(DisabledTextColor), typeof(Color), typeof(Calendar), Color.FromHex("#dddddd"),
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDisabledTextColor((Color)newValue, (Color)oldValue));

		protected void ChangeDisabledTextColor(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => !b.IsEnabled).ForEach(b => b.TextColor = newValue);
		}

        /// <summary>
        /// Получает или задает цвет текста отключенных дат.
        /// </summary>
        /// <value>Цвет отключенного текста.</value>
        public Color DisabledTextColor
		{
			get => (Color)GetValue(DisabledTextColorProperty);
            set => SetValue(DisabledTextColorProperty, value);
        }

		#endregion

		#region Размер шрифта

		public static readonly BindableProperty DisabledFontSizeProperty =
			BindableProperty.Create(nameof(DisabledFontSize), typeof(double), typeof(Calendar), 20.0,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDisabledFontSize((double)newValue, (double)oldValue));

		protected void ChangeDisabledFontSize(double newValue, double oldValue)
		{
			if (Math.Abs(newValue - oldValue) < 0.01) return;
			_buttons.FindAll(b => !b.IsEnabled).ForEach(b => b.FontSize = newValue);
		}

        /// <summary>
        /// Получает или задает размер шрифта для отключенных дат.
        /// </summary>
        /// <value>Размер отключенного шрифта.</value>
        public double DisabledFontSize
		{
			get => (double)GetValue(DisabledFontSizeProperty);
            set => SetValue(DisabledFontSizeProperty, value);
        }

		#endregion

		#region Атрибуты отключенного шрифта

		public static readonly BindableProperty DisabledFontAttributesProperty =
			BindableProperty.Create(nameof(DisabledFontAttributes), typeof(FontAttributes), typeof(Calendar), FontAttributes.None,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDisabledFontAttributes((FontAttributes)newValue, (FontAttributes)oldValue));

		protected void ChangeDisabledFontAttributes(FontAttributes newValue, FontAttributes oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => !b.IsEnabled).ForEach(b => b.FontAttributes = newValue);
		}

        /// <summary>
        /// Получает или задает семейство шрифтов отключенных дат.
        /// </summary>
        public FontAttributes DisabledFontAttributes
		{
			get => (FontAttributes)GetValue(DisabledFontAttributesProperty);
            set => SetValue(DisabledFontAttributesProperty, value);
        }

		#endregion

		#region Семейство шрифта

		public static readonly BindableProperty DisabledFontFamilyProperty =
			BindableProperty.Create(nameof(DisabledFontFamily), typeof(string), typeof(Calendar), default(string),
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDisabledFontFamily((string)newValue, (string)oldValue));

		protected void ChangeDisabledFontFamily(string newValue, string oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => !b.IsEnabled).ForEach(b => b.FontFamily = newValue);
		}

        /// <summary>
        /// Получает или задает семейство шрифтов отключенных дат.
        /// </summary>
        public string DisabledFontFamily
		{
			get => GetValue(DisabledFontFamilyProperty) as string;
            set => SetValue(DisabledFontFamilyProperty, value);
        }

		#endregion

        /// <summary>
        /// Сделать кнопку отключенной
        /// </summary>
		protected void SetButtonDisabled(CalendarButton button)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				button.BackgroundPattern = null;
				button.BackgroundImage = null;
				button.FontSize = DisabledFontSize;
				button.BorderWidth = DisabledBorderWidth;
				button.BorderColor = DisabledBorderColor;
				button.BackgroundColor = DisabledBackgroundColor;
				button.TextColor = DisabledTextColor;
				button.FontAttributes = DisabledFontAttributes;
				button.FontFamily = DisabledFontFamily;
                if (Device.RuntimePlatform == Device.UWP)
                {
                    button.IsEnabled = true;
                }
                else
                {
                    button.IsEnabled = false;
                }
				button.IsSelected = false;
			});
	    } // SetButtonDisabled
    }
}