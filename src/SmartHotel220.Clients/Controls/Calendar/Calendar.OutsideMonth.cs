using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    // Календарь вне месяца
	public partial class Calendar
	{

		#region Цвет текста дат

		public static readonly BindableProperty DatesTextColorOutsideMonthProperty =
			BindableProperty.Create(nameof(DatesTextColorOutsideMonth), typeof(Color), typeof(Calendar), Color.FromHex("#aaaaaa"),
			    propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDatesTextColorOutsideMonth((Color)newValue, (Color)oldValue));

		protected void ChangeDatesTextColorOutsideMonth(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => b.IsEnabled && !b.IsSelected && b.IsOutOfMonth).ForEach(b => b.TextColor = newValue);
		}

        /// <summary>
        /// Получает или задает цвет текста дат не в целенаправленном месяце.
        /// </summary>
        public Color DatesTextColorOutsideMonth
		{
			get => (Color)GetValue(DatesTextColorOutsideMonthProperty);
            set => SetValue(DatesTextColorOutsideMonthProperty, value);
        }

		#endregion

		#region Цвет фона дат

		public static readonly BindableProperty DatesBackgroundColorOutsideMonthProperty =
			BindableProperty.Create(nameof(DatesBackgroundColorOutsideMonth), typeof(Color), typeof(Calendar), Color.White,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDatesBackgroundColorOutsideMonth((Color)newValue, (Color)oldValue));

		protected void ChangeDatesBackgroundColorOutsideMonth(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => b.IsEnabled && !b.IsSelected && b.IsOutOfMonth).ForEach(b => b.BackgroundColor = newValue);
		}

        /// <summary>
        /// Возвращает или задает цвет фона для дат не в целенаправленном месяце.
        /// </summary>
        public Color DatesBackgroundColorOutsideMonth
		{
            get => (Color) GetValue(DatesBackgroundColorOutsideMonthProperty);
            set => SetValue(DatesBackgroundColorOutsideMonthProperty, value);
        }

		#endregion

		#region Аттрибуты шрифта

		public static readonly BindableProperty DatesFontAttributesOutsideMonthProperty =
			BindableProperty.Create(nameof(DatesFontAttributesOutsideMonth), typeof(FontAttributes), typeof(Calendar), FontAttributes.None,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDatesFontAttributesOutsideMonth((FontAttributes)newValue, (FontAttributes)oldValue));

		protected void ChangeDatesFontAttributesOutsideMonth(FontAttributes newValue, FontAttributes oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => b.IsEnabled && !b.IsSelected && b.IsOutOfMonth).ForEach(b => b.FontAttributes = newValue);
		}

        /// <summary>
        /// Возвращает или задает атрибуты шрифтов дат за пределами месяца.
        /// </summary>
        public FontAttributes DatesFontAttributesOutsideMonth
		{
			get => (FontAttributes)GetValue(DatesFontAttributesOutsideMonthProperty);
            set => SetValue(DatesFontAttributesOutsideMonthProperty, value);
        }

		#endregion

		#region Семейство шрифта

		public static readonly BindableProperty DatesFontFamilyOutsideMonthProperty =
			BindableProperty.Create(nameof(DatesFontFamilyOutsideMonth), typeof(string), typeof(Calendar), default(string),
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDatesFontFamilyOutsideMonth((string)newValue, (string)oldValue));

		protected void ChangeDatesFontFamilyOutsideMonth(string newValue, string oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => b.IsEnabled && !b.IsSelected && b.IsOutOfMonth).ForEach(b => b.FontFamily = newValue);
		}

        /// <summary>
        /// Возвращает или задает семейства шрифтов для дат вне месяца.
        /// </summary>
        public string DatesFontFamilyOutsideMonth
		{
			get => GetValue(DatesFontFamilyOutsideMonthProperty) as string;
            set => SetValue(DatesFontFamilyOutsideMonthProperty, value);
        }

		#endregion
	}
}
