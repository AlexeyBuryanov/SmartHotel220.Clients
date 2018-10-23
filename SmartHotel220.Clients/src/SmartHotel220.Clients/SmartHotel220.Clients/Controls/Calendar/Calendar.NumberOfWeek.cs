using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    // Календарь. Кол-во недель
    public partial class Calendar
    {
	    private readonly List<Grid> _weekNumbers;
	    private readonly List<Label> _weekNumberLabels;

		#region Цвет текста

		public static readonly BindableProperty NumberOfWeekTextColorProperty =
		    BindableProperty.Create(nameof(NumberOfWeekTextColor), typeof(Color), typeof(Calendar), Color.FromHex("#aaaaaa"),
			    propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeNumberOfWeekTextColor((Color)newValue, (Color)oldValue));

		protected void ChangeNumberOfWeekTextColor(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_weekNumberLabels.ForEach(l => l.TextColor = newValue);
		}

        /// <summary>
        /// Возвращает или задает цвет текста для числа меток недели.
        /// </summary>
        /// <value>Цвет текста.</value>
        public Color NumberOfWeekTextColor
		{
			get => (Color)GetValue(NumberOfWeekTextColorProperty);
            set => SetValue(NumberOfWeekTextColorProperty, value);
        }

		public static readonly BindableProperty NumberOfWeekBackgroundColorProperty =
            BindableProperty.Create(nameof(NumberOfWeekBackgroundColor), typeof(Color), typeof(Calendar), Color.Transparent,
			    propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeNumberOfWeekBackgroundColor((Color)newValue, (Color)oldValue));

		protected void ChangeNumberOfWeekBackgroundColor(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_weekNumberLabels.ForEach(l => l.BackgroundColor = newValue);
		}

        /// <summary>
        /// Получает или задает цвет фона для количества меток недели.
        /// </summary>
        /// <value>Цвет фона.</value>
        public Color NumberOfWeekBackgroundColor
		{
			get => (Color)GetValue(NumberOfWeekBackgroundColorProperty);
            set => SetValue(NumberOfWeekBackgroundColorProperty, value);
        }

		#endregion

		#region Размер шрифта

		public static readonly BindableProperty NumberOfWeekFontSizeProperty =
			BindableProperty.Create(nameof(NumberOfWeekFontSize), typeof(double), typeof(Calendar), 14.0,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeNumberOfWeekFontSize((double)newValue, (double)oldValue));

		protected void ChangeNumberOfWeekFontSize(double newValue, double oldValue)
		{
			if (Math.Abs(newValue - oldValue) < 0.01) return;
			_weekNumbers?.ForEach(obj => obj.WidthRequest = newValue*(2.2));
			_weekNumberLabels.ForEach(l => l.FontSize = newValue);
		}

        /// <summary>
        /// Возвращает или задает размер шрифта для числа меток недели.
        /// </summary>
        /// <value>Размер шрифта.</value>
        public double NumberOfWeekFontSize
		{
			get => (double)GetValue(NumberOfWeekFontSizeProperty);
            set => SetValue(NumberOfWeekFontSizeProperty, value);
        }

		#endregion

		#region Атрибуты шрифта

		public static readonly BindableProperty NumberOfWeekFontAttributesProperty =
			BindableProperty.Create(nameof(NumberOfWeekFontAttributes), typeof(FontAttributes), typeof(Calendar), FontAttributes.None,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeNumberOfWeekFontAttributes((FontAttributes)newValue, (FontAttributes)oldValue));

		protected void ChangeNumberOfWeekFontAttributes(FontAttributes newValue, FontAttributes oldValue)
		{
			if (newValue == oldValue) return;
			_weekNumberLabels.ForEach(l => l.FontAttributes = newValue);
		}

        /// <summary>
        /// Получает или задает атрибуты шрифта для числа меток недели.
        /// </summary>
        public FontAttributes NumberOfWeekFontAttributes
		{
			get => (FontAttributes)GetValue(NumberOfWeekFontAttributesProperty);
            set => SetValue(NumberOfWeekFontAttributesProperty, value);
        }

		#endregion

		#region Семейство шрифта

		public static readonly BindableProperty NumberOfWeekFontFamilyProperty =
			BindableProperty.Create(nameof(NumberOfWeekFontFamily), typeof(string), typeof(Calendar), default(string),
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeNumberOfWeekFontFamily((string)newValue, (string)oldValue));

		protected void ChangeNumberOfWeekFontFamily(string newValue, string oldValue)
		{
			if (newValue == oldValue) return;
			_weekNumberLabels.ForEach(l => l.FontFamily = newValue);
		}

        /// <summary>
        /// Получает или задает семейство шрифтов числа меток недели.
        /// </summary>
        public string NumberOfWeekFontFamily
		{
			get => GetValue(NumberOfWeekFontFamilyProperty) as string;
            set => SetValue(NumberOfWeekFontFamilyProperty, value);
        }

		#endregion

		#region Показать кол-во недель

		public static readonly BindableProperty ShowNumberOfWeekProperty =
		    BindableProperty.Create(nameof(ShowNumberOfWeek), typeof(bool), typeof(Calendar), false,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ShowHideElements());

        /// <summary>
        /// Возвращает или задает, показывать ли количество меток недели.
        /// </summary>
        public bool ShowNumberOfWeek
		{
			get => (bool)GetValue(ShowNumberOfWeekProperty);
            set => SetValue(ShowNumberOfWeekProperty, value);
        }

		#endregion

		#region Недельные правила

		public static readonly BindableProperty CalendarWeekRuleProperty =
			BindableProperty.Create(nameof(CalendarWeekRule), typeof(CalendarWeekRule), typeof(Calendar), CalendarWeekRule.FirstFourDayWeek,
			    propertyChanged: (bindable, oldValue, newValue) =>
			    {
				    var calendar = bindable as Calendar;
				    var start = calendar.CalendarStartDate(calendar.StartDate).Date;

				    for (var i = 0; i < calendar._buttons.Count; i++)
				    {
					    calendar.ChangeWeekNumbers(start, i);

					    start = start.AddDays(1);
					    if (i != 0 && (i + 1) % 42 == 0)
					    {
						    start = calendar.CalendarStartDate(start);
					    }
				    }
			    });

        /// <summary>
        /// Получает или задает, какой CalendarWeekRule использовать для чисел недели
        /// </summary>
        public CalendarWeekRule CalendarWeekRule
		{
			get => (CalendarWeekRule)GetValue(CalendarWeekRuleProperty);
            set => SetValue(CalendarWeekRuleProperty, value);
        }

		#endregion

		protected void ChangeWeekNumbers(DateTime start, int i)
		{
			if (!ShowNumberOfWeek) return;
			var ciCurr = CultureInfo.CurrentCulture;
			var weekNum = ciCurr.Calendar.GetWeekOfYear(start, CalendarWeekRule.FirstFourDayWeek, StartDay);
			_weekNumberLabels[(i / 7)].Text = string.Format("{0}", weekNum);
		}

        /// <summary>
        /// Показать/скрыть элементы
        /// </summary>
		protected void ShowHideElements()
		{
			if (_mainCalendars.Count < 1) return;

			_contentView.Children.Clear();
			_dayLabels.Clear();

			for (var i = 0; i < ShowNumOfMonths; i++)
			{
				var main = _mainCalendars[i] as Layout;

				if (ShowInBetweenMonthLabels && i > 0)
				{
					var label = new Label
					{
						FontSize = TitleLabel.FontSize,
						VerticalTextAlignment = TitleLabel.VerticalTextAlignment,
						HorizontalTextAlignment = TitleLabel.HorizontalTextAlignment,
						FontAttributes = TitleLabel.FontAttributes,
						TextColor = TitleLabel.TextColor,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Text = string.Empty
					};
					if (_titleLabels == null)
					{
						_titleLabels = new List<Label>(ShowNumOfMonths);
					}

                    _titleLabels.Add(label);
                    _contentView.Children.Add(label);
				}

				if (ShowNumberOfWeek)
				{
					main = new StackLayout
					{
						Padding = 0,
						Spacing = 0,
						VerticalOptions = LayoutOptions.FillAndExpand,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Orientation = StackOrientation.Horizontal,
						Children = { _weekNumbers[i], _mainCalendars[i] }
					};
				}

				if (WeekdaysShow)
				{
					var columDef = new ColumnDefinition { Width = 48 };
				    var dl = new Grid
				    {
				        VerticalOptions = LayoutOptions.CenterAndExpand,
				        RowSpacing = 0,
				        ColumnSpacing = 0,
				        Padding = 0,
				        ColumnDefinitions = new ColumnDefinitionCollection
				        {
				            columDef,
				            columDef,
				            columDef,
				            columDef,
				            columDef,
				            columDef,
				            columDef
				        }
				    };

				    var marginFront = NumberOfWeekFontSize * 1.5;
                    if (Device.RuntimePlatform == Device.UWP) marginFront = NumberOfWeekFontSize * 3;
                    if (ShowNumberOfWeek) dl.Padding = new Thickness(marginFront, 0, 0, 0);

                    for (var c = 0; c < 7; c++)
					{
                        _dayLabels.Add(new Label
                        {
                            HeightRequest = 48,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center,
							BackgroundColor = WeekdaysBackgroundColor,
							TextColor = WeekdaysTextColor,
							FontSize = WeekdaysFontSize,
							FontFamily = WeekdaysFontFamily,
							FontAttributes = WeekdaysFontAttributes
						});
						dl.Children.Add(_dayLabels.Last(), c, 0);
					}

					var stack = new StackLayout
					{
						Padding = 0,
						Spacing = 0,
						VerticalOptions = LayoutOptions.FillAndExpand,
						HorizontalOptions = LayoutOptions.Center,
						Orientation = StackOrientation.Vertical,
						Children = { dl, main }
					};
					_contentView.Children.Add(stack);
				}
				else
				{
					_contentView.Children.Add(main);
				}
			} // for i
		} // ShowHideElements
	}
}
