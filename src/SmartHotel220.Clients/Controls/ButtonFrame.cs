using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    /// <inheritdoc />
    /// <summary>
    /// Фрейм для кнопки
    /// </summary>
    public class ButtonFrame : Frame
    {
        public ButtonFrame()
        {
            // Включаем тень
            HasShadow = true;
        }

        /// <summary>
        /// При изменении св-ва. Переопределённый метод Frame
        /// </summary>
        protected override void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            // Если имя св-ва совпадает с именем свойства контента
            if (propertyName == ContentProperty.PropertyName)
            {
                ContentUpdated();
            }
        }

        /// <summary>
        /// Обновление контента
        /// </summary>
        private void ContentUpdated()
        {
            if (Device.RuntimePlatform != Device.UWP)
            {
                BackgroundColor = Content.BackgroundColor;
            }
        }
    }
}
