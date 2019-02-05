using Android.Content;
using SmartHotel220.Clients.Core.Controls;
using SmartHotel220.Clients.Droid.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace SmartHotel220.Clients.Droid.Renderers
{
    /// <summary>
    /// Рендер текст-бокса
    /// </summary>
    public class ExtendedEntryRenderer : EntryRenderer
    {
        public ExtendedEntryRenderer(Context context) : base(context)
        {
        }

        public ExtendedEntry ExtendedEntryElement => Element as ExtendedEntry;

        /// <summary>
        /// При изменении элемента
        /// </summary>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            // Если есть новый элемент
            if (e.NewElement != null)
            {
                // Меняем тип ввода, если его нет
                Control.InputType |= Android.Text.InputTypes.TextFlagNoSuggestions;
                // Обновляем цвет линии
                UpdateLineColor();
            }
        }

        /// <summary>
        /// При изменении св-ва привязки
        /// </summary>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            // Если цвет линии применён (LineColorToApply)
            if (e.PropertyName.Equals(nameof(ExtendedEntry.LineColorToApply)))
            {
                // Обновляем
                UpdateLineColor();
            }
        }

        private void UpdateLineColor()
        {
            Control?.Background?.SetColorFilter(ExtendedEntryElement.LineColorToApply.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcAtop);
        }
    }
}