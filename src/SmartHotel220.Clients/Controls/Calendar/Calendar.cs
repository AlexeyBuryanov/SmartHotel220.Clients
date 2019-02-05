using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    /// <inheritdoc />
    /// <summary>
    /// Календарь для выбора дат
    /// </summary>
	public partial class Calendar : ContentView
	{
        /// <summary>
        /// Кнопки
        /// </summary>
	    private readonly List<CalendarButton> _buttons;
        /// <summary>
        /// Основные календари
        /// </summary>
	    private readonly List<Grid> _mainCalendars;
        /// <summary>
        /// Заголовки
        /// </summary>
	    private List<Label> _titleLabels;
        // mainView - основной вид, contentView - контент
	    private readonly StackLayout _mainView, _contentView;
        public static double GridSpace = 0;
        
        public Calendar()
		{
            // Создаём кнопку в заголовке со стрелкой влево
			TitleLeftArrow = new CalendarButton
			{
                FontAttributes = FontAttributes.None,
                BackgroundColor = Color.Transparent,
                BackgroundImage = ImageSource.FromFile(Device.RuntimePlatform == Device.UWP ? "Assets/ic_arrow_left_normal.png" : "ic_arrow_left_normal") as FileImageSource,
                HeightRequest = 48,
                WidthRequest = 48,
                Margin = new Thickness(24, 0, 0, 0),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End
			};

            // Создаём в заголовке лэйбл
			TitleLabel = new Label
			{ 
				FontSize = 24,
				HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.None,
				TextColor = Color.Black,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = string.Empty,
                LineBreakMode = LineBreakMode.NoWrap
            };

		    // Создаём кнопку в заголовке со стрелкой вправо
            TitleRightArrow = new CalendarButton
            {
                FontAttributes = FontAttributes.None,
                BackgroundColor = Color.Transparent,
                HeightRequest = 48,
                WidthRequest = 48,
                Margin = new Thickness(0, 0, 24, 0),
                BackgroundImage = ImageSource.FromFile((Device.RuntimePlatform == Device.UWP) ? "Assets/ic_arrow_right_normal.png" : "ic_arrow_right_normal") as FileImageSource,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start
			};

            // Навигация по месяцам (разметка)
			MonthNavigationLayout = new StackLayout
			{
				Padding = 0,
				VerticalOptions = LayoutOptions.Start,
				Orientation = StackOrientation.Horizontal,
				HeightRequest = 50,
				Children = { TitleLeftArrow, TitleLabel, TitleRightArrow}
			};

			_contentView = new StackLayout
			{
				Padding = 0,
				Orientation = StackOrientation.Vertical
			};

			_mainView = new StackLayout {
				Padding = 0,
				Orientation = StackOrientation.Vertical,
				Children = { MonthNavigationLayout, _contentView }
			};
            
            // Назначаем обработчики по клику
			TitleLeftArrow.Clicked += LeftArrowClickedEvent;
			TitleRightArrow.Clicked += RightArrowClickedEvent;

			_dayLabels = new List<Label>(7);
			_weekNumberLabels = new List<Label>(6);
			_buttons = new List<CalendarButton>(42);
			_mainCalendars = new List<Grid>(1);
			_weekNumbers = new List<Grid>(1);

			CalendarViewType = DateTypeEnum.Normal;
			YearsRow = 4;
			YearsColumn = 4;
		}

		#region Минимальная дата

		public static readonly BindableProperty MinDateProperty =
			BindableProperty.Create(nameof(MinDate), typeof(DateTime?), typeof(Calendar), null,
	            propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeCalendar(CalandarChanges.MaxMin));

        /// <summary>
        /// Возвращает или задает минимальную дату.
        /// </summary>
        /// <value>Минимальная дата.</value>
        public DateTime? MinDate
		{
			get => (DateTime?)GetValue(MinDateProperty);
            set {
                SetValue(MinDateProperty, value);
                ChangeCalendar(CalandarChanges.MaxMin);
            }
		}

		#endregion

		#region Максимальная дата

		public static readonly BindableProperty MaxDateProperty =
			BindableProperty.Create(nameof(MaxDate), typeof(DateTime?), typeof(Calendar), null,		
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeCalendar(CalandarChanges.MaxMin));

        /// <summary>
        /// Возвращает или задает максимальную дату.
        /// </summary>
        /// <value>Максимальная дата.</value>
        public DateTime? MaxDate
		{
			get => (DateTime?)GetValue(MaxDateProperty);
            set => SetValue(MaxDateProperty, value);
        }

		#endregion

		#region Начальная дата

		public static readonly BindableProperty StartDateProperty =
			BindableProperty.Create(nameof(StartDate), typeof(DateTime), typeof(Calendar), DateTime.Now,
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeCalendar(CalandarChanges.StartDate));

        /// <summary>
        /// Получает или задает дату, чтобы выбрать месяц
        /// </summary>
        /// <value>The start date.</value>
        public DateTime StartDate
		{
			get => (DateTime)GetValue(StartDateProperty);
            set => SetValue(StartDateProperty, value);
        }

		#endregion

		#region Начальный день

		public static readonly BindableProperty StartDayProperty =
			BindableProperty.Create(nameof(StartDate), typeof(DayOfWeek), typeof(Calendar), DayOfWeek.Sunday,	
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeCalendar(CalandarChanges.StartDay));

        /// <summary>
        /// Возвращает или задает день, в который календар начинает неделю.
        /// </summary>
        /// <value>Начальный день.</value>
        public DayOfWeek StartDay
		{
			get => (DayOfWeek)GetValue(StartDayProperty);
            set => SetValue(StartDayProperty, value);
        }

		#endregion

		#region Ширина рамки

		public static readonly BindableProperty BorderWidthProperty =
			BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(Calendar), 3,
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeBorderWidth((int)newValue, (int)oldValue));

		protected void ChangeBorderWidth(int newValue, int oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => !b.IsSelected && b.IsEnabled).ForEach(b => b.BorderWidth = newValue);
		}

        /// <summary>
        /// Получает или задает ширину границы календаря.
        /// </summary>
        /// <value>Ширина границы.</value>
        public int BorderWidth
		{
			get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        #endregion

	    #region Ширина внешней границы

        public static readonly BindableProperty OuterBorderWidthProperty =
			BindableProperty.Create(nameof(OuterBorderWidth), typeof(int), typeof(Calendar), 3,
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?._mainCalendars.ForEach(obj => obj.Padding = (int)newValue));

        /// <summary>
        /// Возвращает или задает ширину всей границы календаря.
        /// </summary>
        /// <value>Ширина внешней границы.</value>
        public int OuterBorderWidth
		{
			get => (int)GetValue(OuterBorderWidthProperty);
            set => SetValue(OuterBorderWidthProperty, value);
        }

        #endregion

	    #region Цвет границы

        public static readonly BindableProperty BorderColorProperty =
			BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(Calendar), Color.FromHex("#dddddd"),
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeBorderColor((Color)newValue, (Color)oldValue));

		protected void ChangeBorderColor(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_mainCalendars.ForEach(obj => obj.BackgroundColor = newValue);
			_buttons.FindAll(b => b.IsEnabled && !b.IsSelected).ForEach(b => b.BorderColor = newValue);
		}

        /// <summary>
        /// Возвращает или задает цвет рамки календаря.
        /// </summary>
        /// <value>Цвет границы.</value>
        public Color BorderColor
		{
			get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

		#endregion

		#region Фоновый цвет дат

		public static readonly BindableProperty DatesBackgroundColorProperty =
			BindableProperty.Create(nameof(DatesBackgroundColor), typeof(Color), typeof(Calendar), Color.White,
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDatesBackgroundColor((Color)newValue, (Color)oldValue));

		protected void ChangeDatesBackgroundColor(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => b.IsEnabled && (!b.IsSelected || SelectedBackgroundColor != Color.Default)).ForEach(b => b.BackgroundColor = newValue);
		}

        /// <summary>
        /// Возвращает или задает цвет фона нормальных дат.
        /// </summary>
        /// <value>Цвет фона дат.</value>
        public Color DatesBackgroundColor
		{
			get => (Color)GetValue(DatesBackgroundColorProperty);
            set => SetValue(DatesBackgroundColorProperty, value);
        }

		#endregion

		#region Цвет текста дат

		public static readonly BindableProperty DatesTextColorProperty =
			BindableProperty.Create(nameof(DatesTextColor), typeof(Color), typeof(Calendar), Color.Black,	
                propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDatesTextColor((Color)newValue, (Color)oldValue));

		protected void ChangeDatesTextColor(Color newValue, Color oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => b.IsEnabled && (!b.IsSelected || SelectedTextColor != Color.Default) && !b.IsOutOfMonth).ForEach(b => b.TextColor = newValue);
		}

        /// <summary>
        /// Возвращает или задает цвет текста нормальных дат.
        /// </summary>
        /// <value>Цвет текста даты.</value>
        public Color DatesTextColor
		{
			get => (Color)GetValue(DatesTextColorProperty);
            set => SetValue(DatesTextColorProperty, value);
        }

		#endregion

		#region Атрибуты шрифта для дат

		public static readonly BindableProperty DatesFontAttributesProperty =
			BindableProperty.Create(nameof(DatesFontAttributes), typeof(FontAttributes), typeof(Calendar), FontAttributes.None,
			    propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDatesFontAttributes((FontAttributes)newValue, (FontAttributes)oldValue));

		protected void ChangeDatesFontAttributes(FontAttributes newValue, FontAttributes oldValue)
		{
			if (newValue == oldValue) return;
			_buttons.FindAll(b => b.IsEnabled && (!b.IsSelected || SelectedTextColor != Color.Default) && !b.IsOutOfMonth).ForEach(b => b.FontAttributes = newValue);
		}

        /// <summary>
        /// Возвращает или задает атрибуты шрифтов дат.
        /// </summary>
        /// <value>Атрибуты шрифтов дат.</value>
        public FontAttributes DatesFontAttributes
		{
			get => (FontAttributes)GetValue(DatesFontAttributesProperty);
            set => SetValue(DatesFontAttributesProperty, value);
        }

		#endregion

		#region Размер шрифта дат

		public static readonly BindableProperty DatesFontSizeProperty =
			BindableProperty.Create(nameof(DatesFontSize), typeof(double), typeof(Calendar), 20.0,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDatesFontSize((double)newValue, (double)oldValue));

		protected void ChangeDatesFontSize(double newValue, double oldValue)
		{
			if (Math.Abs(newValue - oldValue) < 0.01) return;
			_buttons?.FindAll(b => !b.IsSelected && b.IsEnabled).ForEach(b => b.FontSize = newValue);
		}

        /// <summary>
        /// Возвращает или задает размер шрифта для нормальных дат.
        /// </summary>
        /// <value>Размер шрифта даты.</value>
        public double DatesFontSize
		{
			get => (double)GetValue(DatesFontSizeProperty);
            set => SetValue(DatesFontSizeProperty, value);
        }

		#endregion

		#region DatesFontFamily

		public static readonly BindableProperty DatesFontFamilyProperty =
			BindableProperty.Create(nameof(DatesFontFamily), typeof(string), typeof(Calendar), default(string),
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeDatesFontFamily((string)newValue, (string)oldValue));

		protected void ChangeDatesFontFamily(string newValue, string oldValue)
		{
			if (newValue == oldValue) return;
			_buttons?.FindAll(b => !b.IsSelected && b.IsEnabled).ForEach(b => b.FontFamily = newValue);
		}

        /// <summary>
        /// Получает или задает семейство шрифтов дат.
        /// </summary>
        public string DatesFontFamily
		{
			get => GetValue(DatesFontFamilyProperty) as string;
            set => SetValue(DatesFontFamilyProperty, value);
        }

		#endregion

		#region Показать кол-во месяцев

		public static readonly BindableProperty ShowNumOfMonthsProperty =
			BindableProperty.Create(nameof(ShowNumOfMonths), typeof(int), typeof(Calendar), 1,
			    propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeCalendar(CalandarChanges.All));

        /// <summary>
        /// Получает или задает количество месяцев на показ.
        /// </summary>
        /// <value>Дата начала.</value>
        public int ShowNumOfMonths
		{
			get => (int)GetValue(ShowNumOfMonthsProperty);
            set => SetValue(ShowNumOfMonthsProperty, value);
        }

		#endregion

		#region Показать между месячные лэйблы

		public static readonly BindableProperty ShowInBetweenMonthLabelsProperty =
			BindableProperty.Create(nameof(ShowInBetweenMonthLabels), typeof(bool), typeof(Calendar), true,
				propertyChanged: (bindable, oldValue, newValue) => (bindable as Calendar)?.ChangeCalendar(CalandarChanges.All));

        /// <summary>
        /// Получает или задает количество месяцев на показ.
        /// </summary>
        /// <value>Дата начала.</value>
        public bool ShowInBetweenMonthLabels
		{
			get => (bool)GetValue(ShowInBetweenMonthLabelsProperty);
            set => SetValue(ShowInBetweenMonthLabelsProperty, value);
        }

		#endregion

		#region Команда

        public static readonly BindableProperty DateCommandProperty =
            BindableProperty.Create(nameof(DateCommand), typeof(ICommand), typeof(Calendar), null);

        /// <summary>
        /// Возвращает или задает выбранную команду даты.
        /// </summary>
        /// <value>Команда даты.</value>
        public ICommand DateCommand
        {
            get => (ICommand)GetValue(DateCommandProperty);
            set => SetValue(DateCommandProperty, value);
        }

        #endregion

        /// <summary>
        /// Дата начала календаря.
        /// </summary>
        public DateTime CalendarStartDate(DateTime date)
		{
			var start = date;
            // Начало месяца
			var beginOfMonth = start.Day == 1;

            // Пока не начало месяца или день недели не равен начальному дню недели
			while (!beginOfMonth || start.DayOfWeek != StartDay)
			{
				start = start.AddDays(-1);
				beginOfMonth |= start.Day == 1;
			}

			return start;
		}

		#region Функции

        /// <summary>
        /// Вызывается, когда родительский элемент изменяется.
        /// </summary>
		protected override void OnParentSet()
		{
            // Заполнить календарь
            FillCalendarWindows();
			base.OnParentSet();
            // Изменить календарь
			ChangeCalendar(CalandarChanges.All);
		}

        /// <summary>
        /// Заполнить календарь
        /// </summary>
		protected Task FillCalendar()
		{
			return Task.Factory.StartNew(FillCalendarWindows);
		}

		protected void FillCalendarWindows()
		{
			CreateWeeknumbers();
			CreateButtons();
			ShowHideElements();
		}

        /// <summary>
        /// Создать номера недель
        /// </summary>
		protected void CreateWeeknumbers()
		{
			_weekNumberLabels.Clear();
			_weekNumbers.Clear();

			if (!ShowNumberOfWeek) return;

            // Проходим по кол-ву отображённых месяцев
			for (var i = 0; i < ShowNumOfMonths; i++)
			{
				var columDef = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
				var rowDef = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
			    var weekNumbers = new Grid
			    {
			        VerticalOptions = LayoutOptions.FillAndExpand,
			        HorizontalOptions = LayoutOptions.Start,
			        RowSpacing = 0,
			        ColumnSpacing = 0,
			        Padding = new Thickness(0, 0, 0, 0),
			        ColumnDefinitions = new ColumnDefinitionCollection { columDef },
			        RowDefinitions = new RowDefinitionCollection
			        {
			            rowDef,
			            rowDef,
			            rowDef,
			            rowDef,
			            rowDef,
			            rowDef
			        },
			        WidthRequest = NumberOfWeekFontSize * (Device.RuntimePlatform == Device.iOS ? 1.5 : 2.5)
			    };

			    for (var r = 0; r < 6; r++)
				{
					_weekNumberLabels.Add(new Label
					{
						HorizontalOptions = LayoutOptions.FillAndExpand,
						VerticalOptions = LayoutOptions.FillAndExpand,
						TextColor = NumberOfWeekTextColor,
						BackgroundColor = NumberOfWeekBackgroundColor,
						VerticalTextAlignment = TextAlignment.Center,
						HorizontalTextAlignment = TextAlignment.Center,
						FontSize = NumberOfWeekFontSize,
						FontAttributes = NumberOfWeekFontAttributes,
						FontFamily = NumberOfWeekFontFamily
					});

					weekNumbers.Children.Add(_weekNumberLabels.Last(), 0, r);
				}
				_weekNumbers.Add(weekNumbers);
            } // for i ShowNumOfMonths
        }

        /// <summary>
        /// Создать кнопки
        /// </summary>
		protected void CreateButtons()
		{
			var columDef = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) };
			var rowDef = new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) };
			_buttons.Clear();
			_mainCalendars.Clear();

			for (var i = 0; i < ShowNumOfMonths; i++)
			{
			    var mainCalendar = new Grid
			    {
			        VerticalOptions = LayoutOptions.FillAndExpand,
			        HorizontalOptions = LayoutOptions.CenterAndExpand,
			        RowSpacing = GridSpace,
			        ColumnSpacing = GridSpace,
			        Padding = 1,
			        BackgroundColor = BorderColor,
			        ColumnDefinitions = new ColumnDefinitionCollection
			        {
			            columDef,
			            columDef,
			            columDef,
			            columDef,
			            columDef,
			            columDef,
			            columDef
			        },
			        RowDefinitions = new RowDefinitionCollection
			        {
			            rowDef,
			            rowDef,
			            rowDef,
			            rowDef,
			            rowDef,
			            rowDef
			        }
			    };
                
			    for (var r = 0; r < 5; r++)
				{
					for (var c = 0; c < 7; c++)
					{
						_buttons.Add(new CalendarButton
						{
							BorderRadius = 0,
							BorderWidth = BorderWidth,
							BorderColor = BorderColor,
							FontSize = DatesFontSize,
							BackgroundColor = DatesBackgroundColor,
							TextColor = DatesTextColor,
							FontAttributes = DatesFontAttributes,
							FontFamily = DatesFontFamily,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							VerticalOptions = LayoutOptions.FillAndExpand
						});

						var b = _buttons.Last();
						b.Clicked += DateClickedEvent;
						mainCalendar.Children.Add(b, c, r);
					} // for c
				} // for r
				_mainCalendars.Add(mainCalendar);
            } // for i ShowNumOfMonths
        }

        /// <summary>
        /// Принудительно перерисовать
        /// </summary>
		public void ForceRedraw()
		{
			ChangeCalendar(CalandarChanges.All);
		}

        /// <summary>
        /// Изменить календарь
        /// </summary>
        protected void ChangeCalendar(CalandarChanges changes)
        {
            // В главном потоке UI
			Device.BeginInvokeOnMainThread(() =>
			{
				Content = null;

				if (changes.HasFlag(CalandarChanges.StartDate))
				{
					TitleLabel.Text = StartDate.ToString(TitleLabelFormat);

					if (_titleLabels != null)
					{
						var tls = StartDate.AddMonths(1);

						foreach (var tl in _titleLabels)
						{
							tl.Text = tls.ToString(TitleLabelFormat);
							tls = tls.AddMonths(1);
						} // foreach
                    } // if
				} // if

				var start = CalendarStartDate(StartDate).Date;
				var beginOfMonth = false;
				var endOfMonth = false;
				for (var i = 0; i < _buttons.Count; i++)
				{
					endOfMonth |= beginOfMonth && start.Day == 1;
					beginOfMonth |= start.Day == 1;

					if (i < _dayLabels.Count && WeekdaysShow && changes.HasFlag(CalandarChanges.StartDay))
                    {
                        var day = start.ToString(WeekdaysFormat);
                        var showDay = char.ToUpper(day.First()) + day.Substring(1).ToLower();
                        _dayLabels[i].Text = showDay;
					}

					ChangeWeekNumbers(start, i);

					if (changes.HasFlag(CalandarChanges.All))
					{
						_buttons[i].Text = string.Format("{0}", start.Day);
					}
					else
					{
						_buttons[i].TextWithoutMeasure = string.Format("{0}", start.Day);
					}
					_buttons[i].Date = start;

					_buttons[i].IsOutOfMonth = !(beginOfMonth && !endOfMonth);
					_buttons[i].IsEnabled = ShowNumOfMonths == 1 || !_buttons[i].IsOutOfMonth;

					SpecialDate sd = null;
					if (SpecialDates != null)
					{
						sd = SpecialDates.FirstOrDefault(s => s.Date.Date == start.Date);
					}

                    SetButtonNormal(_buttons[i]);

					if ((MinDate.HasValue && start.Date < MinDate) || (MaxDate.HasValue && start.Date > MaxDate) || (DisableAllDates && sd == null))
					{
						SetButtonDisabled(_buttons[i]);
					}
					else if (_buttons[i].IsEnabled && (SelectedDates?.Select(d => d.Date)?.Contains(start.Date) ?? false))
					{
						SetButtonSelected(_buttons[i], sd);
					}
					else if (sd != null)
					{
						SetButtonSpecial(_buttons[i], sd);
					}

                    start = start.AddDays(1);

                    if (i != 0 && (i+1) % 42 == 0)
					{
						beginOfMonth = false;
						endOfMonth = false;
						start = CalendarStartDate(start);
					}

				} // for i

				if (DisableDatesLimitToMaxMinRange)
				{
					TitleLeftArrow.IsEnabled = !(MinDate.HasValue && CalendarStartDate(StartDate).Date < MinDate);
					TitleRightArrow.IsEnabled = !(MaxDate.HasValue && start > MaxDate);
				}

				Content = _mainView;
			});
        }

        /// <summary>
        /// Сделать кнопку нормальной
        /// </summary>
        protected void SetButtonNormal(CalendarButton button)
        {
			button.BackgroundPattern = null;
			button.BackgroundImage = null;
                
            Device.BeginInvokeOnMainThread(() =>
            {
                button.IsEnabled = true;
                button.IsSelected = false;
				button.FontSize = DatesFontSize;
                button.BorderWidth = BorderWidth;
                button.BorderColor = BorderColor;
				button.FontFamily = button.IsOutOfMonth ? DatesFontFamilyOutsideMonth : DatesFontFamily;
                button.BackgroundColor = button.IsOutOfMonth ? DatesBackgroundColorOutsideMonth : DatesBackgroundColor;
                button.TextColor = button.IsOutOfMonth ? DatesTextColorOutsideMonth : DatesTextColor;
				button.FontAttributes = button.IsOutOfMonth ? DatesFontAttributesOutsideMonth : DatesFontAttributes;
				button.IsEnabled = ShowNumOfMonths == 1 || !button.IsOutOfMonth;
            });
        }

        /// <summary>
        /// Событие по клику даты
        /// </summary>
		protected void DateClickedEvent(object s, EventArgs a)
		{
			var selectedDate = ((CalendarButton)s).Date;
			if (SelectedDate.HasValue && selectedDate.HasValue && SelectedDate.Value == selectedDate.Value)
			{
				ChangeSelectedDate(selectedDate);
				SelectedDate = null;
			}
			else
			{
				SelectedDate = selectedDate;
			}
		}

#endregion

        /// <summary>
        /// Событие "нажатая дата"
        /// </summary>
        public event EventHandler<DateTimeEventArgs> DateClicked;
    }
}