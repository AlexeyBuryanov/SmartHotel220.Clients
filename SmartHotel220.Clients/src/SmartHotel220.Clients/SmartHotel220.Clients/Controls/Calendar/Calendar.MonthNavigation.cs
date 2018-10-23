using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    // Календарь. Навигация по месяцам
	public partial class Calendar
	{
		#region Заголовок

		public static readonly BindableProperty TitleLabelHorizontalTextAlignmentProperty = 
		    BindableProperty.Create(nameof(TitleLabelHorizontalTextAlignment), typeof(TextAlignment), typeof(Calendar), default(TextAlignment),
			    propertyChanged: (bindable, oldValue, newValue) =>
			    {
				    ((Calendar)bindable).TitleLabel.HorizontalTextAlignment = (TextAlignment)newValue;
				    ((Calendar)bindable)?._titleLabels?.ForEach(l => l.HorizontalTextAlignment = (TextAlignment)newValue);
			    });

		public TextAlignment TitleLabelHorizontalTextAlignment
		{
			get => TitleLabel.HorizontalTextAlignment;
		    set => TitleLabel.HorizontalTextAlignment = value;
		}

		public static readonly BindableProperty TitleLabelVerticalTextAlignmentProperty = 
		    BindableProperty.Create(nameof(TitleLabelVerticalTextAlignment), typeof(TextAlignment), typeof(Calendar), default(TextAlignment),
		        propertyChanged: (bindable, oldValue, newValue) => 
		        {
			        ((Calendar)bindable).TitleLabel.VerticalTextAlignment = (TextAlignment)newValue;
			        ((Calendar)bindable)?._titleLabels?.ForEach(l => l.VerticalTextAlignment = (TextAlignment)newValue);
		        });

		public TextAlignment TitleLabelVerticalTextAlignment
		{
			get => TitleLabel.VerticalTextAlignment;
		    set => TitleLabel.VerticalTextAlignment = value;
		}

		public static readonly BindableProperty TitleLabelTextColorProperty = 
		    BindableProperty.Create(nameof(TitleLabelTextColor), typeof(Color), typeof(Calendar), default(Color),
			    propertyChanged: (bindable, oldValue, newValue) => 
			    {
			        ((Calendar)bindable).TitleLabel.TextColor = (Color)newValue;
			        ((Calendar)bindable)?._titleLabels?.ForEach(l => l.TextColor = (Color)newValue);
		        });

		public Color TitleLabelTextColor
		{
			get => TitleLabel.TextColor;
		    set => TitleLabel.TextColor = value;
		}

		public static readonly BindableProperty TitleLabelTextProperty = 
		    BindableProperty.Create(nameof(TitleLabelText), typeof(string), typeof(Calendar), default(string),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar) bindable).TitleLabel.Text = (string)newValue);

		public string TitleLabelText
		{
			get => TitleLabel.Text;
		    set => TitleLabel.Text = value;
		}

		public static readonly BindableProperty TitleLabelFontFamilyProperty = 
		    BindableProperty.Create(nameof(TitleLabelFontFamily), typeof(string), typeof(Calendar), default(string),
		        propertyChanged: (bindable, oldValue, newValue) => 
		        {
			        ((Calendar)bindable).TitleLabel.FontFamily = (string)newValue;
			        ((Calendar)bindable)?._titleLabels?.ForEach(l => l.FontFamily = (string)newValue);
		        });

		public string TitleLabelFontFamily
		{
			get => TitleLabel.FontFamily;
		    set => TitleLabel.FontFamily = value;
		}

		public static readonly BindableProperty TitleLabelFontSizeProperty = 
		    BindableProperty.Create(nameof(TitleLabelFontSize), typeof(double), typeof(Calendar), default(double),
			    propertyChanged: (bindable, oldValue, newValue) => 
			    {
			        ((Calendar)bindable).TitleLabel.FontSize = (double)newValue;
			        ((Calendar)bindable)?._titleLabels?.ForEach(l => l.FontSize = (double)newValue);
		        });

		public double TitleLabelFontSize
		{
			get => TitleLabel.FontSize;
		    set => TitleLabel.FontSize = value;
		}

		public static readonly BindableProperty TitleLabelFontAttributesProperty = 
		    BindableProperty.Create(nameof(TitleLabelFontAttributes), typeof(FontAttributes), typeof(Calendar), default(FontAttributes),
		        propertyChanged: (bindable, oldValue, newValue) => 
		        {
			        ((Calendar)bindable).TitleLabel.FontAttributes = (FontAttributes)newValue;
			        ((Calendar)bindable)?._titleLabels?.ForEach(l => l.FontAttributes = (FontAttributes)newValue);
		        });

		public FontAttributes TitleLabelFontAttributes
		{
			get => TitleLabel.FontAttributes;
		    set => TitleLabel.FontAttributes = value;
		}

		public static readonly BindableProperty TitleLabelFormattedTextProperty = 
		    BindableProperty.Create(nameof(TitleLabelFormattedText), typeof(FormattedString), typeof(Calendar), default(FormattedString),
		    propertyChanged: (bindable, oldValue, newValue) => 
		    {
			    ((Calendar)bindable).TitleLabel.FormattedText = (FormattedString)newValue;
			    ((Calendar)bindable)?._titleLabels?.ForEach(l => l.FormattedText = (FormattedString)newValue);
		    });

		public FormattedString TitleLabelFormattedText
		{
			get => TitleLabel.FormattedText;
		    set => TitleLabel.FormattedText = value;
		}

		public static readonly BindableProperty TitleLabelLineBreakModeProperty = 
		    BindableProperty.Create(nameof(TitleLabelLineBreakMode), typeof(LineBreakMode), typeof(Calendar), default(LineBreakMode),
		    propertyChanged: (bindable, oldValue, newValue) => 
		    {
			    ((Calendar)bindable).TitleLabel.LineBreakMode = (LineBreakMode)newValue;
			    ((Calendar)bindable)?._titleLabels?.ForEach(l => l.LineBreakMode = (LineBreakMode)newValue);
		    });

		public LineBreakMode TitleLabelLineBreakMode
		{
			get => TitleLabel.LineBreakMode;
		    set => TitleLabel.LineBreakMode = value;
		}

        /// <summary>
        /// Получает заголовок (Label) в навигации по месяцам.
        /// </summary>
        public Label TitleLabel { get; protected set; }

        #endregion

        #region Заголовок (левая стрелка)

        public static readonly BindableProperty TitleLeftArrowTextProperty = 
            BindableProperty.Create(nameof(TitleLeftArrowText), typeof(string), typeof(Calendar), default(string),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLeftArrow.Text = (string)newValue);

		public string TitleLeftArrowText
		{
			get => TitleLeftArrow.Text;
		    set => TitleLeftArrow.Text = value;
		}

		public static readonly BindableProperty TitleLeftArrowTextColorProperty = 
		    BindableProperty.Create(nameof(TitleLeftArrowTextColor), typeof(Color), typeof(Calendar), default(Color),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLeftArrow.TextColor = (Color)newValue);

		public Color TitleLeftArrowTextColor
		{
			get => TitleLeftArrow.TextColor;
		    set => TitleLeftArrow.TextColor = value;
		}

		public static readonly BindableProperty TitleLeftArrowBackgroundColorProperty = 
		    BindableProperty.Create(nameof(TitleLeftArrowBackgroundColor), typeof(Color), typeof(Calendar), default(Color),
			    propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLeftArrow.BackgroundColor = (Color)newValue);

		public Color TitleLeftArrowBackgroundColor
		{
			get => TitleLeftArrow.BackgroundColor;
		    set => TitleLeftArrow.BackgroundColor = value;
		}

		public static readonly BindableProperty TitleLeftArrowFontProperty = 
		    BindableProperty.Create(nameof(TitleLeftArrowFont), typeof(Font), typeof(Calendar), default(Font),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLeftArrow.Font = (Font)newValue);

		public Font TitleLeftArrowFont
		{
			get => TitleLeftArrow.Font;
		    set => TitleLeftArrow.Font = value;
		}

		public static readonly BindableProperty TitleLeftArrowFontFamilyProperty = 
		    BindableProperty.Create(nameof(TitleLeftArrowFontFamily), typeof(string), typeof(Calendar), default(string),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLeftArrow.FontFamily = (string)newValue);

		public string TitleLeftArrowFontFamily
		{
			get => TitleLeftArrow.FontFamily;
		    set => TitleLeftArrow.FontFamily = value;
		}

		public static readonly BindableProperty TitleLeftArrowFontSizeProperty = 
		    BindableProperty.Create(nameof(TitleLeftArrowFontSize), typeof(double), typeof(Calendar), default(double),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLeftArrow.FontSize = (double)newValue);

		public double TitleLeftArrowFontSize
		{
			get => TitleLeftArrow.FontSize;
		    set => TitleLeftArrow.FontSize = value;
		}

		public static readonly BindableProperty TitleLeftArrowFontAttributesProperty = 
		    BindableProperty.Create(nameof(TitleLeftArrowFontAttributes), typeof(FontAttributes), typeof(Calendar), default(FontAttributes),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLeftArrow.FontAttributes = (FontAttributes)newValue);

		public FontAttributes TitleLeftArrowFontAttributes
		{
			get => TitleLeftArrow.FontAttributes;
		    set => TitleLeftArrow.FontAttributes = value;
		}

		public static readonly BindableProperty TitleLeftArrowBorderWidthProperty = 
		    BindableProperty.Create(nameof(TitleLeftArrowBorderWidth), typeof(double), typeof(Calendar), default(double),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLeftArrow.BorderWidth = (double)newValue);

		public double TitleLeftArrowBorderWidth
		{
			get => TitleLeftArrow.BorderWidth;
		    set => TitleLeftArrow.BorderWidth = value;
		}

		public static readonly BindableProperty TitleLeftArrowBorderColorProperty = 
		    BindableProperty.Create(nameof(TitleLeftArrowBorderColor), typeof(Color), typeof(Calendar), default(Color),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLeftArrow.BorderColor = (Color)newValue);

		public Color TitleLeftArrowBorderColor
		{
			get => TitleLeftArrow.BorderColor;
		    set => TitleLeftArrow.BorderColor = value;
		}

		public static readonly BindableProperty TitleLeftArrowBorderRadiusProperty = 
		    BindableProperty.Create(nameof(TitleLeftArrowBorderRadius), typeof(int), typeof(Calendar), default(int),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLeftArrow.BorderRadius = (int)newValue);

		public int TitleLeftArrowBorderRadius
		{
			get => TitleLeftArrow.BorderRadius;
		    set => TitleLeftArrow.BorderRadius = value;
		}

		public static readonly BindableProperty TitleLeftArrowImageProperty = 
		    BindableProperty.Create(nameof(TitleLeftArrowImage), typeof(FileImageSource), typeof(Calendar), default(FileImageSource),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLeftArrow.Image = (FileImageSource)newValue);

		public FileImageSource TitleLeftArrowImage
		{
			get => TitleLeftArrow.Image;
		    set => TitleLeftArrow.Image = value;
		}

		public static readonly BindableProperty TitleLeftArrowIsEnabledCoreProperty = 
		    BindableProperty.Create(nameof(TitleLeftArrowIsEnabled), typeof(bool), typeof(Calendar), default(bool),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLeftArrow.IsEnabled = (bool)newValue);

		public bool TitleLeftArrowIsEnabled
		{
			get => TitleLeftArrow.IsEnabled;
		    set => TitleLeftArrow.IsEnabled = value;
		}

		public static readonly BindableProperty TitleLeftArrowIsVisibleCoreProperty = 
		    BindableProperty.Create(nameof(TitleLeftArrowIsVisible), typeof(bool), typeof(Calendar), default(bool),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLeftArrow.IsVisible = (bool)newValue);

		public bool TitleLeftArrowIsVisible
		{
			get => TitleLeftArrow.IsVisible;
		    set => TitleLeftArrow.IsVisible = value;
		}

        /// <summary>
        /// Возвращает левую кнопку навигации по месяцам.
        /// </summary>
        public CalendarButton TitleLeftArrow { get; protected set; }

        #endregion

        #region Заголовок (правая стрелка)

        public static readonly BindableProperty TitleRightArrowTextProperty = 
            BindableProperty.Create(nameof(TitleRightArrowText), typeof(string), typeof(Calendar), default(string),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleRightArrow.Text = (string)newValue);

		public string TitleRightArrowText
		{
			get => TitleRightArrow.Text;
		    set => TitleRightArrow.Text = value;
		}

		public static readonly BindableProperty TitleRightArrowTextColorProperty = 
		    BindableProperty.Create(nameof(TitleRightArrowTextColor), typeof(Color), typeof(Calendar), default(Color),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleRightArrow.TextColor = (Color)newValue);

		public Color TitleRightArrowTextColor
		{
			get => TitleRightArrow.TextColor;
		    set => TitleRightArrow.TextColor = value;
		}

		public static readonly BindableProperty TitleRightArrowBackgroundColorProperty = 
		    BindableProperty.Create(nameof(TitleRightArrowBackgroundColor), typeof(Color), typeof(Calendar), default(Color),
			    propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleRightArrow.BackgroundColor = (Color)newValue);

		public Color TitleRightArrowBackgroundColor
		{
			get => TitleRightArrow.BackgroundColor;
		    set => TitleRightArrow.BackgroundColor = value;
		}

		public static readonly BindableProperty TitleRightArrowFontProperty = 
		    BindableProperty.Create(nameof(TitleRightArrowFont), typeof(Font), typeof(Calendar), default(Font),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleRightArrow.Font = (Font)newValue);

		public Font TitleRightArrowFont
		{
			get => TitleRightArrow.Font;
		    set => TitleRightArrow.Font = value;
		}

		public static readonly BindableProperty TitleRightArrowFontFamilyProperty = 
		    BindableProperty.Create(nameof(TitleRightArrowFontFamily), typeof(string), typeof(Calendar), default(string),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleRightArrow.FontFamily = (string)newValue);

		public string TitleRightArrowFontFamily
		{
			get => TitleRightArrow.FontFamily;
		    set => TitleRightArrow.FontFamily = value;
		}

		public static readonly BindableProperty TitleRightArrowFontSizeProperty = 
		    BindableProperty.Create(nameof(TitleRightArrowFontSize), typeof(double), typeof(Calendar), default(double),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleRightArrow.FontSize = (double)newValue);

		public double TitleRightArrowFontSize
		{
			get => TitleRightArrow.FontSize;
		    set => TitleRightArrow.FontSize = value;
		}

		public static readonly BindableProperty TitleRightArrowFontAttributesProperty = 
		    BindableProperty.Create(nameof(TitleRightArrowFontAttributes), typeof(FontAttributes), typeof(Calendar), default(FontAttributes),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleRightArrow.FontAttributes = (FontAttributes)newValue);

		public FontAttributes TitleRightArrowFontAttributes
		{
			get => TitleRightArrow.FontAttributes;
		    set => TitleRightArrow.FontAttributes = value;
		}

		public static readonly BindableProperty TitleRightArrowBorderWidthProperty = 
		    BindableProperty.Create(nameof(TitleRightArrowBorderWidth), typeof(double), typeof(Calendar), default(double),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleRightArrow.BorderWidth = (double)newValue);

		public double TitleRightArrowBorderWidth
		{
			get => TitleRightArrow.BorderWidth;
		    set => TitleRightArrow.BorderWidth = value;
		}

		public static readonly BindableProperty TitleRightArrowBorderColorProperty = 
		    BindableProperty.Create(nameof(TitleRightArrowBorderColor), typeof(Color), typeof(Calendar), default(Color),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleRightArrow.BorderColor = (Color)newValue);

		public Color TitleRightArrowBorderColor
		{
			get => TitleRightArrow.BorderColor;
		    set => TitleRightArrow.BorderColor = value;
		}

		public static readonly BindableProperty TitleRightArrowBorderRadiusProperty = 
		    BindableProperty.Create(nameof(TitleRightArrowBorderRadius), typeof(int), typeof(Calendar), default(int),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleRightArrow.BorderRadius = (int)newValue);

		public int TitleRightArrowBorderRadius
		{
			get => TitleRightArrow.BorderRadius;
		    set => TitleRightArrow.BorderRadius = value;
		}

		public static readonly BindableProperty TitleRightArrowImageProperty = 
		    BindableProperty.Create(nameof(TitleRightArrowImage), typeof(FileImageSource), typeof(Calendar), default(FileImageSource),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleRightArrow.Image = (FileImageSource)newValue);

		public FileImageSource TitleRightArrowImage
		{
			get => TitleRightArrow.Image;
		    set => TitleRightArrow.Image = value;
		}

		public static readonly BindableProperty TitleRightArrowIsEnabledCoreProperty = 
		    BindableProperty.Create(nameof(TitleRightArrowIsEnabled), typeof(bool), typeof(Calendar), default(bool),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleRightArrow.IsEnabled = (bool)newValue);

		public bool TitleRightArrowIsEnabled
		{
			get => TitleRightArrow.IsEnabled;
		    set => TitleRightArrow.IsEnabled = value;
		}

		public static readonly BindableProperty TitleRightArrowIsVisibleCoreProperty = 
		    BindableProperty.Create(nameof(TitleRightArrowIsVisible), typeof(bool), typeof(Calendar), default(bool),
				propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleRightArrow.IsVisible = (bool)newValue);

		public bool TitleRightArrowIsVisible
		{
			get => TitleRightArrow.IsVisible;
		    set => TitleRightArrow.IsVisible = value;
		}

        /// <summary>
        /// Возвращает правую кнопку навигации по месяцам.
        /// </summary>
        public CalendarButton TitleRightArrow { get; protected set; }

        #endregion

        /// <summary>
        /// Возвращает правую кнопку навигации по месяцам.
        /// </summary>
        public StackLayout MonthNavigationLayout { get; protected set; }

		#region Отображаемая навигация по месяцам

		public static readonly BindableProperty MonthNavigationShowProperty =
			BindableProperty.Create(nameof(MonthNavigationShow), typeof(bool), typeof(Calendar), true,
			    propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).MonthNavigationLayout.IsVisible = (bool)newValue);

        /// <summary>
        /// Получает или задает, следует ли показывать навигацию месяца.
        /// </summary>
        /// <value>Отображать ли навигацию по месяцам.</value>
        public bool MonthNavigationShow
		{
			get => (bool)GetValue(MonthNavigationShowProperty);
            set => SetValue(MonthNavigationShowProperty, value);
        }

        #endregion

        #region Формат метки (Label) заголовка

        public static readonly BindableProperty TitleLabelFormatProperty =
			BindableProperty.Create(nameof(TitleLabelFormat), typeof(string), typeof(Calendar), "MMMM\nyyyy",
			    propertyChanged: (bindable, oldValue, newValue) => ((Calendar)bindable).TitleLabel.Text = ((Calendar) bindable).StartDate.ToString((string)newValue));

        /// <summary>
        /// Возвращает или задает формат заголовка в навигации по месяцам.
        /// </summary>
        /// <value>Формат метки заголовка.</value>
        public string TitleLabelFormat
		{
			get => GetValue(TitleLabelFormatProperty) as string;
            set => SetValue(TitleLabelFormatProperty, value);
        }

		#endregion

		#region Включить заголовок подробностей по месяца и годам

		public static readonly BindableProperty EnableTitleMonthYearDetailsProperty =
		    BindableProperty.Create(nameof(EnableTitleMonthYearView), typeof(bool), typeof(Calendar), false,
			    propertyChanged: (bindable, oldValue, newValue) =>
			    {
                    // Очищаем все жесты
				    (bindable as Calendar)?.TitleLabel.GestureRecognizers.Clear();
				    if (!(bool)newValue) return;
                    // Создаём новый тап-жест
				    var gr = new TapGestureRecognizer();
				    gr.Tapped += (sender, e) => (bindable as Calendar)?.NextMonthYearView();
                    // Привязываем новый жест
				    (bindable as Calendar)?.TitleLabel.GestureRecognizers.Add(gr);
			    });

        /// <summary>
        /// Получает или задает, отображается ли в заголовке месяц, год или нормальный вид
        /// </summary>
        /// <value>Показывает дни недели.</value>
        public bool EnableTitleMonthYearView
		{
			get => (bool)GetValue(EnableTitleMonthYearDetailsProperty);
            set => SetValue(EnableTitleMonthYearDetailsProperty, value);
        }

		#endregion

        /// <summary>
        /// Ивенты по клику на левую и правую стрелку
        /// </summary>
		public event EventHandler<DateTimeEventArgs> RightArrowClicked, LeftArrowClicked;

		#region Команда для правой стрелки

		public static readonly BindableProperty RightArrowCommandProperty =
			BindableProperty.Create(nameof(RightArrowCommand), typeof(ICommand), typeof(Calendar), null);

		public ICommand RightArrowCommand
		{
			get => (ICommand)GetValue(RightArrowCommandProperty);
		    set => SetValue(RightArrowCommandProperty, value);
		}

		protected void RightArrowClickedEvent(object s, EventArgs a)
		{
            // Если тип календаря годичный, то переключаем года, иначе месяца
			if (CalendarViewType == DateTypeEnum.Year)
			{
				NextPrevYears(true);
			}
			else 
			{
				NextMonth();
			}
			RightArrowClicked?.Invoke(s, new DateTimeEventArgs { DateTime = StartDate });
			RightArrowCommand?.Execute(StartDate);
		}
		
		public void NextMonth() 
		{
			StartDate = new DateTime(StartDate.Year, StartDate.Month, 1).AddMonths(ShowNumOfMonths);
		}

		#endregion

		#region Команда для левой стрелки

		public static readonly BindableProperty LeftArrowCommandProperty =
			BindableProperty.Create(nameof(LeftArrowCommand), typeof(ICommand), typeof(Calendar), null);

		public ICommand LeftArrowCommand
		{
			get => (ICommand)GetValue(LeftArrowCommandProperty);
		    set => SetValue(LeftArrowCommandProperty, value);
		}

		protected void LeftArrowClickedEvent(object s, EventArgs a)
		{
		    // Если тип календаря годичный, то переключаем года, иначе месяца
            if (CalendarViewType == DateTypeEnum.Year)
			{
				NextPrevYears(false);
			}
			else
			{
				PreviousMonth();
			}
			LeftArrowClicked?.Invoke(s, new DateTimeEventArgs { DateTime = StartDate });
			LeftArrowCommand?.Execute(StartDate);
		}
		
		public void PreviousMonth()
		{
		    StartDate = new DateTime(StartDate.Year, StartDate.Month, 1).AddMonths(-ShowNumOfMonths);
		}
		#endregion
	}
}
