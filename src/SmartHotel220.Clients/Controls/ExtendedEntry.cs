using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    /// <inheritdoc />
    /// <summary>
    /// Кастомный текст-бокс (Entry - запись)
    /// </summary>
    public class ExtendedEntry : Entry
    {
        /// <summary>
        /// Цвет линии при применении
        /// </summary>
        private Color _lineColorToApply;

        public ExtendedEntry()
        {
            // Назначаем события фокуса
            Focused += OnFocused;
            Unfocused += OnUnfocused;

            // Сбрасываем цвет линии
            ResetLineColor();
        }

        /// <summary>
        /// Цвет линии при применении
        /// </summary>
        public Color LineColorToApply
        {
            get => _lineColorToApply;
            private set 
            {
                _lineColorToApply = value;
                OnPropertyChanged(nameof(LineColorToApply));
            }
        }

        /// <summary>
        /// Цвет линии (св-во привязки)
        /// </summary>
        public static readonly BindableProperty LineColorProperty =
            BindableProperty.Create("LineColor", typeof(Color), typeof(ExtendedEntry), Color.Default);

        public Color LineColor
        {
            get => (Color)GetValue(LineColorProperty);
            set => SetValue(LineColorProperty, value);
        }

        /// <summary>
        /// Цвет линии при фокусе (св-во привязки)
        /// </summary>
        public static readonly BindableProperty FocusLineColorProperty =
            BindableProperty.Create("FocusLineColor", typeof(Color), typeof(ExtendedEntry), Color.Default);

        public Color FocusLineColor
        {
            get => (Color)GetValue(FocusLineColorProperty);
            set => SetValue(FocusLineColorProperty, value);
        }

        /// <summary>
        /// Валидно ли (св-во привязки)
        /// </summary>
        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create("IsValid", typeof(bool), typeof(ExtendedEntry), true);

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        /// <summary>
        /// Цвет линии при не валидной ситуации
        /// </summary>
        public static readonly BindableProperty InvalidLineColorProperty =
            BindableProperty.Create("InvalidLineColor", typeof(Color), typeof(ExtendedEntry), Color.Default);

        public Color InvalidLineColor
        {
            get => (Color)GetValue(InvalidLineColorProperty);
            set => SetValue(InvalidLineColorProperty, value);
        }

        /// <summary>
        /// При изменении св-ва
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            // Если это было св-во валидации, то запустить проверку валидации
            if (propertyName == IsValidProperty.PropertyName)
            {
                CheckValidity();
            }
        }

        /// <summary>
        /// При установке фокуса
        /// </summary>
        private void OnFocused(object sender, FocusEventArgs e)
        {
            IsValid = true;
            LineColorToApply = FocusLineColor != Color.Default
                ? FocusLineColor
                : GetNormalStateLineColor();
        }

        /// <summary>
        /// При сбросе фокуса
        /// </summary>
        private void OnUnfocused(object sender, FocusEventArgs e)
        {
            ResetLineColor();
        }

        /// <summary>
        /// Сбросить цвет линии
        /// </summary>
        private void ResetLineColor()
        {
            LineColorToApply = GetNormalStateLineColor();
        }

        /// <summary>
        /// Проверить действительность
        /// </summary>
        private void CheckValidity()
        {
            if (!IsValid)
            {
                LineColorToApply = InvalidLineColor;
            }
        }

        /// <summary>
        /// Получить нормальное состояние цвета линии
        /// </summary>
        private Color GetNormalStateLineColor()
        {
            // Если цвет линии не равен дефолту, то возвращаем цвет линии,
            // а иначе просто цвет текста
            return LineColor != Color.Default
                    ? LineColor
                    : TextColor;
        }
    }
}