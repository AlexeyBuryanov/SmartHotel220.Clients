using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    // Календарь. Выбранный
    public partial class Calendar
    {
        #region Выбранная дата

        public static readonly BindableProperty SelectedDateProperty =
            BindableProperty.Create(nameof(SelectedDate), typeof(DateTime?), typeof(Calendar), null, BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    if (((Calendar)bindable).ChangeSelectedDate(newValue as DateTime?))
                    {
                        ((Calendar)bindable).SelectedDate = null;
                    }
                });

        /// <summary>
        /// Получает или задает дату выбранной даты
        /// </summary>
        public DateTime? SelectedDate
		{
			get => (DateTime?)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value.HasValue ? value.Value.Date : value);
        }

        #endregion

        #region Выбранный диапазон
        public static readonly BindableProperty SelectRangeProperty = 
            BindableProperty.Create(nameof(SelectRange), typeof(bool), typeof(Calendar), false);

        /// <summary>
        /// Получает или задает выбор несколько дат.
        /// </summary>
        public bool SelectRange
        {
            get => (bool)GetValue(SelectRangeProperty);
            set => SetValue(SelectRangeProperty, value);
        }
        #endregion

        #region Множественный выбор дат

        public static readonly BindableProperty MultiSelectDatesProperty = 
            BindableProperty.Create(nameof(MultiSelectDates), typeof(bool), typeof(Calendar), false);

        /// <summary>
        /// Получает или задает может ли выбираться множество дат.
        /// </summary>
        public bool MultiSelectDates
		{
			get => (bool)GetValue(MultiSelectDatesProperty);
            set => SetValue(MultiSelectDatesProperty, value);
        }

		public static readonly BindableProperty SelectedDatesProperty = 
            BindableProperty.Create(nameof(SelectedDates), typeof(IList<DateTime>), typeof(Calendar), null,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    if (newValue != null)
                    {
                        ((Calendar)bindable).SelectedDates = newValue as IList<DateTime>;
                        foreach (var date in (bindable as Calendar)?.SelectedDates)
                        {
                            ((Calendar)bindable)?.ChangeSelectedDate(date);
                        }
                    }
                });

        /// <summary>
        /// Получает выбранные даты, когда MultiSelectDates имеет значение true
        /// </summary>
        public IList<DateTime> SelectedDates
		{
			get => (IList<DateTime>)GetValue(SelectedDatesProperty);
            protected set => SetValue(SelectedDatesProperty, value);
        }

        #endregion

        #region Ширина бордера (границы)

        public static readonly BindableProperty SelectedBorderWidthProperty =
			BindableProperty.Create(nameof(SelectedBorderWidth), typeof(int), typeof(Calendar), 5,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar).ChangeSelectedBorderWidth((int)newValue, (int)oldValue));

		protected void ChangeSelectedBorderWidth(int newValue, int oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => b.IsSelected).ForEach(b => b.BorderWidth = newValue);
		}

        /// <summary>
        /// Возвращает или задает ширину границы выбранной даты.
        /// </summary>
        public int SelectedBorderWidth
		{
			get => (int)GetValue(SelectedBorderWidthProperty);
            set => SetValue(SelectedBorderWidthProperty, value);
        }

		#endregion

		#region Цвет границы

		public static readonly BindableProperty SelectedBorderColorProperty =
			BindableProperty.Create(nameof(SelectedBorderColor), typeof(Color), typeof(Calendar), Color.FromHex("#c82727"),
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeSelectedBorderColor((Color)newValue, (Color)oldValue));

		protected void ChangeSelectedBorderColor(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => b.IsSelected).ForEach(b => b.BorderColor = newValue);
		}

        /// <summary>
        /// Возвращает или задает цвет выбранной даты.
        /// </summary>
        public Color SelectedBorderColor
		{
			get => (Color)GetValue(SelectedBorderColorProperty);
            set => SetValue(SelectedBorderColorProperty, value);
        }

		#endregion

		#region Цвет фона

		public static readonly BindableProperty SelectedBackgroundColorProperty =
			BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(Calendar), Color.Default,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeSelectedBackgroundColor((Color)newValue, (Color)oldValue));

		protected void ChangeSelectedBackgroundColor(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => b.IsSelected).ForEach(b => b.BackgroundColor = (newValue != Color.Default ?  newValue : Color.Transparent));
		}

        /// <summary>
        /// Возвращает или задает цвет фона выбранной даты.
        /// </summary>
        public Color SelectedBackgroundColor
		{
			get => (Color)GetValue(SelectedBackgroundColorProperty);
            set => SetValue(SelectedBackgroundColorProperty, value);
        }
        #endregion

        #region Фоновое изображение
        public static readonly BindableProperty SelectedBackgroundImageProperty =
            BindableProperty.Create(nameof(SelectedBackgroundImage), typeof(FileImageSource), typeof(Calendar), null);

        public FileImageSource SelectedBackgroundImage {
            get => (FileImageSource)GetValue(SelectedBackgroundImageProperty);
            set => SetValue(SelectedBackgroundImageProperty, value);
        }

        public static readonly BindableProperty SelectedRangeBackgroundImageProperty =
            BindableProperty.Create(nameof(SelectedRangeBackgroundImage), typeof(FileImageSource), typeof(Calendar), null);

        public FileImageSource SelectedRangeBackgroundImage
        {
            get => (FileImageSource)GetValue(SelectedRangeBackgroundImageProperty);
            set => SetValue(SelectedRangeBackgroundImageProperty, value);
        }

        public static readonly BindableProperty FirstSelectedBackgroundImageProperty =
            BindableProperty.Create(nameof(FirstSelectedBackgroundImage), typeof(FileImageSource), typeof(Calendar), null);

        public FileImageSource FirstSelectedBackgroundImage
        {
            get => (FileImageSource)GetValue(FirstSelectedBackgroundImageProperty);
            set => SetValue(FirstSelectedBackgroundImageProperty, value);
        }

        public static readonly BindableProperty LastSelectedBackgroundImageProperty =
            BindableProperty.Create(nameof(LastSelectedBackgroundImage), typeof(FileImageSource), typeof(Calendar), null);

        public FileImageSource LastSelectedBackgroundImage
        {
            get => (FileImageSource)GetValue(LastSelectedBackgroundImageProperty);
            set => SetValue(LastSelectedBackgroundImageProperty, value);
        }

        #endregion

        #region Цвет текста

        public static readonly BindableProperty SelectedTextColorProperty =
			BindableProperty.Create(nameof(SelectedTextColor), typeof(Color), typeof(Calendar), Color.Default,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeSelectedTextColor((Color)newValue, (Color)oldValue));

		protected void ChangeSelectedTextColor(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => b.IsSelected).ForEach(b => b.TextColor = (newValue != Color.Default ?  newValue: Color.Black));
		}

        /// <summary>
        /// Возвращает или задает цвет текста выбранной даты.
        /// </summary>
        public Color SelectedTextColor
		{
			get => (Color)GetValue(SelectedTextColorProperty);
            set => SetValue(SelectedTextColorProperty, value);
        }

		#endregion

		#region Размер шрифта

		public static readonly BindableProperty SelectedFontSizeProperty =
			BindableProperty.Create(nameof(SelectedFontSize), typeof(double), typeof(Calendar), 20.0,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeSelectedFontSize((double)newValue, (double)oldValue));

		protected void ChangeSelectedFontSize(double newValue, double oldValue)
		{
			if (Math.Abs(newValue - oldValue) < 0.01) return;
			_buttons.FindAll(b => b.IsSelected).ForEach(b => b.FontSize = newValue);
		}

        /// <summary>
        /// Возвращает или задает размер шрифта для выбранной даты.
        /// </summary>
        public double SelectedFontSize
		{
			get => (double)GetValue(SelectedFontSizeProperty);
            set => SetValue(SelectedFontSizeProperty, value);
        }

		#endregion

		#region Атрибуты шрифта

		public static readonly BindableProperty SelectedFontAttributesProperty =
			BindableProperty.Create(nameof(SelectedFontAttributes), typeof(FontAttributes), typeof(Calendar), FontAttributes.None,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeSelectedFontAttributes((FontAttributes)newValue, (FontAttributes)oldValue));

		protected void ChangeSelectedFontAttributes(FontAttributes newValue, FontAttributes oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => b.IsSelected).ForEach(b => b.FontAttributes = newValue);
		}

        /// <summary>
        /// Возвращает или задает атрибуты шрифтов дат для выбранных дат.
        /// </summary>
        public FontAttributes SelectedFontAttributes
		{
			get => (FontAttributes)GetValue(SelectedFontAttributesProperty);
            set => SetValue(SelectedFontAttributesProperty, value);
        }

		#endregion

		#region Семейство шрифта

		public static readonly BindableProperty SelectedFontFamilyProperty =
			BindableProperty.Create(nameof(SelectedFontFamily), typeof(string), typeof(Calendar), default(string),
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeSelectedFontFamily((string)newValue, (string)oldValue));

		protected void ChangeSelectedFontFamily(string newValue, string oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => b.IsSelected).ForEach(b => b.FontFamily = newValue);
		}

        /// <summary>
        /// Получает или задает семейство шрифтов для выбранных дат.
        /// </summary>
        public string SelectedFontFamily
		{
			get => GetValue(SelectedFontFamilyProperty) as string;
            set => SetValue(SelectedFontFamilyProperty, value);
        }

		#endregion

        /// <summary>
        /// Сделать кнопку выбранной
        /// </summary>
		protected void SetButtonSelected(CalendarButton button, SpecialDate special, bool first = false, bool last = false)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				button.BackgroundPattern = special?.BackgroundPattern;

                if (first)
                {
                    button.BackgroundImage = special != null ? special.BackgroundImage : FirstSelectedBackgroundImage;
                }
                else if (last)
                {
                    button.BackgroundImage = special != null ? special.BackgroundImage : LastSelectedBackgroundImage;
                }
                else
                {
                    if (SelectedDates.Count == 1)
                    {
				        button.BackgroundImage = special != null ? special.BackgroundImage : SelectedBackgroundImage;
                    }
                    else
                    {
                        button.BackgroundImage = special != null ? special.BackgroundImage : SelectedBackgroundImage != null ? SelectedRangeBackgroundImage : null;
                    }
                }

				var defaultBackgroundColor = button.IsOutOfMonth ? DatesBackgroundColorOutsideMonth : DatesBackgroundColor;
				var defaultTextColor = button.IsOutOfMonth ? DatesTextColorOutsideMonth : DatesTextColor;
				var defaultFontAttributes = button.IsOutOfMonth ? DatesFontAttributesOutsideMonth : DatesFontAttributes;
				var defaultFontFamily = button.IsOutOfMonth ? DatesFontFamilyOutsideMonth : DatesFontFamily;

				button.IsEnabled = true;
				button.IsSelected = true;
                button.VerticalOptions = LayoutOptions.FillAndExpand;
                button.HorizontalOptions = LayoutOptions.FillAndExpand;
                button.FontSize = SelectedFontSize;
				button.BorderWidth = SelectedBorderWidth;
				button.BorderColor = SelectedBorderColor;
				button.BackgroundColor = SelectedBackgroundColor != Color.Default ? SelectedBackgroundColor : (special != null && special.BackgroundColor.HasValue ? special.BackgroundColor.Value : defaultBackgroundColor);
				button.TextColor = SelectedTextColor != Color.Default ? SelectedTextColor : (special != null && special.TextColor.HasValue ? special.TextColor.Value : defaultTextColor);
				button.FontAttributes = SelectedFontAttributes != FontAttributes.None ? SelectedFontAttributes : (special != null && special.FontAttributes.HasValue ? special.FontAttributes.Value : defaultFontAttributes);
				button.FontFamily = !string.IsNullOrEmpty(SelectedFontFamily) ? SelectedFontFamily : (special != null && !string.IsNullOrEmpty(special.FontFamily) ? special.FontFamily :defaultFontFamily);
			});
		}

        /// <summary>
        /// Изменить выбранную дату
        /// </summary>
		protected bool ChangeSelectedDate(DateTime? date, bool clicked = true)
		{
			if (!date.HasValue) return false;

			if (!MultiSelectDates)
			{
				_buttons.FindAll(b => b.IsSelected).ForEach(ResetButton);
				SelectedDates?.Clear();
			}

            if (MinDate.HasValue && date.Value < MinDate.Value) return false;

            if (_buttons.Count == 0)
            {
                SelectedDates?.Add(SelectedDate.Value.Date);
            }

            var button = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == date.Value.Date && b.IsEnabled);

			if (button == null) return false;

			var deselect = button.IsSelected;
			if (button.IsSelected)
			{
                if (SelectRange && SelectedDates?.Count > 2)
                {
                    UnfillSelectedRange(SelectedDate.Value.Date);
                }
                else
                {
                    ResetButton(button);
                    if (SelectedDates?.Count == 1)
                    {
                        button = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == SelectedDates?.First() && b.IsEnabled);
                        if (button != null)
                        {
                            SetButton(button);
                        }
                    }
                }
			}
			else
			{
                if (SelectedDates != null)
                {
                    if (SelectRange && SelectedDates.Any())
                    {
                        FillSelectedRange(SelectedDate.Value.Date);
                    }
                    else
                    {
                        SelectedDates?.Add(SelectedDate.Value.Date);
                        var spD = SpecialDates?.FirstOrDefault(s => s.Date.Date == button.Date.Value.Date);
                        SetButtonSelected(button, spD);
                    }
                }
			}

			if (clicked)
			{
				DateClicked?.Invoke(this, new DateTimeEventArgs { DateTime = SelectedDate.Value });
				DateCommand?.Execute(SelectedDate.Value);
			}

			return deselect;
		}

	    /// <summary>
	    /// Обратно заполнить выбранный диапазон
	    /// </summary>
        public void UnfillSelectedRange(DateTime date)
        {
            var firstDate = SelectedDates.OrderBy(d => d.Date).First();
            var lastDate = SelectedDates.OrderBy(d => d.Date).Last();

            if (date.Equals(firstDate.Date))
            {
                var button = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == date && b.IsEnabled);
                if (button != null) { 
                    ResetButton(button);
                }

                // следующая дата будет последней
                date = date.AddDays(1);
                button = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == date && b.IsEnabled);
                if (button != null)
                {
                    SetButton(button, true);
                }
            }
            else if (date.Equals(lastDate.Date))
            {
                var button = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == date && b.IsEnabled);
                if (button != null)
                {
                    ResetButton(button);
                }

                // предыдущая дата будет последней
                date = date.AddDays(-1);
                button = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == date && b.IsEnabled);
                if (button != null)
                {
                    SetButton(button, false, true);
                }
            }
            else
            {
                // установить дату клика как последнюю дату
                var button = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == date && b.IsEnabled);
                if (button != null)
                {
                    SetButton(button, false, true);
                }

                // отключить вправо
                date = date.AddDays(1);
                while (date <= lastDate.Date)
                {
                    button = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == date && b.IsEnabled);
                    if (button != null)
                    {
                        ResetButton(button);
                    }

                    date = date.AddDays(1);
                }
            }
        }

        /// <summary>
        /// Заполнить выбранный диапазон
        /// </summary>
        public void FillSelectedRange(DateTime date)
        {
            var firstDate = SelectedDates.OrderBy(d => d.Date).First();
            var lastDate = SelectedDates.OrderBy(d => d.Date).Last();

            if (date < firstDate)
            {
                var isFirst = true;
                while (date < firstDate.Date)
                {
                    var button = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == date && b.IsEnabled);
                    if (button != null)
                    {
                        SetButton(button, isFirst);
                        isFirst = false;
                    }

                    date = date.AddDays(1);
                }

                if (date < lastDate)
                {
                    // кнопка изменения
                    var button = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == date && b.IsEnabled);
                    if (button != null)
                    {
                        var spD = SpecialDates?.FirstOrDefault(s => s.Date.Date == button.Date.Value.Date);
                        SetButtonSelected(button, spD);
                    }
                }
            }
            else if (date > lastDate.Date)
            {
                var isLast = true;
                while (date > lastDate.Date)
                {

                    var button2 = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == date && b.IsEnabled);
                    if (button2 != null)
                    {
                        SetButton(button2, false, isLast);
                        isLast = false;
                    }

                    date = date.AddDays(-1);
                }

                if (date > firstDate)
                {
                    // кнопка изменения
                    var button = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == date && b.IsEnabled);
                    if (button != null)
                    {
                        var spD = SpecialDates?.FirstOrDefault(s => s.Date.Date == button.Date.Value.Date);
                        SetButtonSelected(button, spD);
                    }
                }
            }

            // изменить марджин иконок
            SetFirstIcon(SelectedDates.OrderBy(d => d.Date).First());
            SetLastIcon(SelectedDates.OrderBy(d => d.Date).Last());
        }

        /// <summary>
        /// Установить кнопку
        /// </summary>
        protected void SetButton(CalendarButton b, bool isFirst = false, bool isLast = false)
        {
            if (!SelectedDates.Contains(b.Date.Value.Date))
            {
                SelectedDates.Add(b.Date.Value.Date);
            }

            var spD = SpecialDates?.FirstOrDefault(s => s.Date.Date == b.Date.Value.Date);
            SetButtonSelected(b, spD, isFirst, isLast);
        }

        /// <summary>
        /// Установить первую иконку
        /// </summary>
        protected void SetFirstIcon(DateTime date)
        {
            var button = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == date && b.IsEnabled);
            if (button != null)
            {
                var spD = SpecialDates?.FirstOrDefault(s => s.Date.Date == button.Date.Value.Date);
                SetButtonSelected(button, spD, true);
            }
        }

        /// <summary>
        /// Установить последнюю иконку
        /// </summary>
        protected void SetLastIcon(DateTime date)
        {
            var button = _buttons.Find(b => b.Date.HasValue && b.Date.Value.Date == date && b.IsEnabled);
            if (button != null)
            {
                var spD = SpecialDates?.FirstOrDefault(s => s.Date.Date == button.Date.Value.Date);
                SetButtonSelected(button, spD, false, true);
            }
        }

        /// <summary>
        /// Сбросить кнопку
        /// </summary>
        protected void ResetButton(CalendarButton b)
		{
			if (b.Date.HasValue) SelectedDates?.Remove(b.Date.Value.Date);

		    var spD = SpecialDates?.FirstOrDefault(s => s.Date.Date == b.Date.Value.Date);

			SetButtonNormal(b);
			if (spD != null)
			{
				SetButtonSpecial(b, spD);
			}
		}
	}
}