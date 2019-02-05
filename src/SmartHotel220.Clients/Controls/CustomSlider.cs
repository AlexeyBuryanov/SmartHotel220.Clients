using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    /// <inheritdoc />
    /// <summary>
    /// Кастомный слайдер (ползунок)
    /// </summary>
    public class CustomSlider : ContentView
    {
        /// <summary>
        /// Уже распределено
        /// </summary>
        private bool _alreadyAllocated = false;

        public CustomSlider()
        {
            Initialize();
        }

        /// <summary>
        /// Св-во привязки - Минимум
        /// </summary>
        public static readonly BindableProperty MinimumProperty =
            BindableProperty.Create("Minimum", typeof(double), typeof(CustomSlider), 0.0d, propertyChanged: MinimumChanged);

        /// <summary>
        /// Св-во привязки - Максимум
        /// </summary>
        public static readonly BindableProperty MaximumProperty =
            BindableProperty.Create("Maximum", typeof(double), typeof(CustomSlider), 100.0d, propertyChanged: MaximumChanged);

        /// <summary>
        /// Св-во привязки - Текущее значение
        /// </summary>
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create("Value", typeof(double), typeof(CustomSlider), default(double),
                BindingMode.TwoWay, propertyChanged: ValueChanged);

        /// <summary>
        /// Св-во привязки - Фоновая картинка
        /// </summary>
        public static readonly BindableProperty BackgroundImageProperty =
            BindableProperty.Create("BackgroundImage", typeof(ImageSource), typeof(CustomSlider), default(ImageSource),
                BindingMode.TwoWay, propertyChanged: BackgroundImageChanged);

        /// <summary>
        /// Св-во привязки - Картинка индикатора
        /// </summary>
        public static readonly BindableProperty ThumbImageProperty =
            BindableProperty.Create("ThumbImage", typeof(ImageSource), typeof(CustomSlider), default(ImageSource),
                BindingMode.TwoWay, propertyChanged: ThumbImageChanged);

        /// <summary>
        /// Св-во привязки - Конвертер отображаемого значения
        /// </summary>
        public static readonly BindableProperty DisplayConverterProperty =
            BindableProperty.Create("DisplayConverter", typeof(IValueConverter), typeof(CustomSlider), default(IValueConverter),
                BindingMode.OneWay, propertyChanged: DisplayConverterChanged);

        /// <summary>
        /// Максимум
        /// </summary>
        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// Минимум
        /// </summary>
        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// Текущее значение
        /// </summary>
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Фоновая картинка
        /// </summary>
        public ImageSource BackgroundImage
        {
            get => (ImageSource)GetValue(BackgroundImageProperty);
            set => SetValue(BackgroundImageProperty, value);
        }

        /// <summary>
        /// Картинка-индикатор
        /// </summary>
        public ImageSource ThumbImage
        {
            get => (ImageSource)GetValue(ThumbImageProperty);
            set => SetValue(ThumbImageProperty, value);
        }

        /// <summary>
        /// Конвертер значений
        /// </summary>
        public IValueConverter DisplayConverter
        {
            get => (IValueConverter)GetValue(DisplayConverterProperty);
            set => SetValue(DisplayConverterProperty, value);
        }

        /// <summary>
        /// Слайдер (ползунок) (контрол)
        /// </summary>
        protected Slider SliderControl { get; set; }

        /// <summary>
        /// Картинка индикатор (контрол)
        /// </summary>
        protected Image ThumbImageControl { get; set; }

        /// <summary>
        /// Фоновая картинка (контрол)
        /// </summary>
        protected Image BackgroundImageControl { get; set; }

        /// <summary>
        /// Label для отображения значения (контрол)
        /// </summary>
        protected Label ValueControl { get; set; }

        /// <summary>
        /// Контейнер
        /// </summary>
        protected StackLayout ValueContainer { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// При изменении размера
        /// </summary>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // Если ещё не задано и ширина с высотой корректные
            if (!_alreadyAllocated && SliderControl.Bounds.Width > -1 && SliderControl.Bounds.Height > -1)
            {
                // Если это не ПК, то уже выделено. То есть инициализация проводится 1 раз
                if (Device.Idiom != TargetIdiom.Desktop)
                    _alreadyAllocated = true;

                // Двигаем индикатор
                MoveIndicator(SliderControl.Value);
            }
        }

        /// <summary>
        /// Начальная инициализация
        /// </summary>
        private void Initialize()
        {
            // Создаём контейнер с контентом
            var content = new Grid
            {
                Padding = new Thickness(4, 0)
            };

            // Добавляем 2 строки
            content.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            content.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

            // Создаём сам элемент управления
            SliderControl = new Slider
            {
                Minimum = Minimum,
                Maximum = Maximum,
                Value = Value,
                Opacity = 0,
                HeightRequest = 24,
                VerticalOptions = LayoutOptions.Start
            };

            // Назначаем событие при изменении значения
            SliderControl.ValueChanged += (sender, args) => 
            {
                MoveIndicator(args.NewValue);
            };

            // Устанавливаем слайдер в ранее созданную сетку
            Grid.SetRow(SliderControl, 0);
            content.Children.Add(SliderControl);

            // Создаём фоновую картинку
            BackgroundImageControl = new Image
            {
                Margin = new Thickness(-content.Padding.Left / 2, Device.RuntimePlatform == Device.UWP ? 8 : -5,
                    -content.Padding.Right / 2, 0),
                Aspect = Device.Idiom == TargetIdiom.Desktop ? Aspect.AspectFill : Aspect.AspectFit,
                HeightRequest = Device.Idiom == TargetIdiom.Desktop ? 12 : 18,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                InputTransparent = true
            };

            // Устанавливаем картинку в ранее созданную сетку
            Grid.SetRow(BackgroundImageControl, 0);
            content.Children.Add(BackgroundImageControl);

            // Создаём контейнер
            ValueContainer = new StackLayout
            {
                Spacing = 0,
                HorizontalOptions = LayoutOptions.Start,
                InputTransparent = true
            };

            // Создаём картинку-индикатор
            ThumbImageControl = new Image
            {
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = Device.RuntimePlatform == Device.UWP ? 28 : 20,
                InputTransparent = true
            };

            // Добавляем контейнер в сетку
            Grid.SetRow(ValueContainer, 0);
            Grid.SetRowSpan(ValueContainer, 2);
            content.Children.Add(ValueContainer);

            // Добавляем картинку-индикатор в сетку
            ValueContainer.Children.Add(ThumbImageControl);

            // Создаём лэйбл для отображения значения
            ValueControl = new Label
            {
                Margin = new Thickness(0, -6, 0, 0),
                Style = Application.Current.Resources["RobotoMediumLabelStyle"] as Style,
                FontSize = Application.Current.Resources["MidMediumSize"] as double? ?? 12.0,
                HorizontalOptions = LayoutOptions.Center
            };

            // Обновляем отображаемое значение
            UpdateDisplayValue();

            // Добавляем в контейнер созданный лэйбл
            ValueContainer.Children.Add(ValueControl);

            Content = content;
        }

        /// <summary>
        /// Передвинуть индикатор
        /// </summary>
        private void MoveIndicator(double v)
        {
            // Получаем позицию
            var slideIndicatorPosition = CalculatePosition(v);
            // Обновляем отображаемое значение
            UpdateDisplayValue();

            // Половинка индикатора (контрола)
            var thumbHalf = ThumbImageControl.Bounds.Width / 2;
            // Разница (ШиринаКонтейнера - ширинаИндикатора / 2)
            var diff = (ValueContainer.Bounds.Width - ThumbImageControl.Bounds.Width) / 2;

            // Сдвигаем контейнер по иксу
            ValueContainer.TranslationX = slideIndicatorPosition - diff - thumbHalf;
        }

        /// <summary>
        /// Обновить отображаемое значение
        /// </summary>
        private void UpdateDisplayValue()
        {
            // Если есть конвертер, действуем согласно конвертеру
            ValueControl.Text = DisplayConverter != null
                ? $"{DisplayConverter.Convert(SliderControl.Value, typeof(string), null, CultureInfo.CurrentUICulture)}"
                : SliderControl.Value.ToString("N0");
        }

        /// <summary>
        /// Посчитать позицию
        /// </summary>
        private double CalculatePosition(double value)
        {
            // Разница в рэнже
            var rangeDiff = Maximum - Minimum;
            // Разница значения
            var valueDiff = value - Minimum;
            // Соотношение
            var ratio = valueDiff / rangeDiff;

            // Ширина * соотношение = текущая позиция слайдера
            return SliderControl.Bounds.Width * ratio;
        }

        /// <summary>
        /// При изменении значения
        /// </summary>
        private static void ValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customSlider = (CustomSlider)bindable;

            customSlider.SliderControl.Value = Convert.ToDouble(newValue);
            customSlider.CalculatePosition(Convert.ToDouble(newValue));
        }

        /// <summary>
        /// При изменении фонового изображения
        /// </summary>
        private static void BackgroundImageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customSlider = (CustomSlider)bindable;
            customSlider.BackgroundImageControl.Source = (ImageSource)newValue;
        }

        /// <summary>
        /// При изменении изображения индикатора
        /// </summary>
        private static void ThumbImageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customSlider = (CustomSlider)bindable;
            customSlider.ThumbImageControl.Source = (ImageSource)newValue;
        }

        /// <summary>
        /// При изменении минимального значения
        /// </summary>
        private static void MinimumChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customSlider = (CustomSlider)bindable;
            customSlider.SliderControl.Minimum = (double)newValue;
        }

        /// <summary>
        /// При изменении максимального значения
        /// </summary>
        private static void MaximumChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customSlider = (CustomSlider)bindable;
            customSlider.SliderControl.Maximum = (double)newValue;
        }

        /// <summary>
        /// При изменении конвертера
        /// </summary>
        private static void DisplayConverterChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customSlider = (CustomSlider)bindable;
            customSlider?.UpdateDisplayValue();
        }
    }
}