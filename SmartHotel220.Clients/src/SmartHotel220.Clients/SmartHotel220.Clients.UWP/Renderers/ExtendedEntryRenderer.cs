using SmartHotel220.Clients.Core.Controls;
using SmartHotel220.Clients.UWP.Extensions;
using SmartHotel220.Clients.UWP.Renderers;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace SmartHotel220.Clients.UWP.Renderers
{
    /// <summary>
    /// Рендерер текст-бокса
    /// </summary>
    public class ExtendedEntryRenderer : EntryRenderer
    {
        public ExtendedEntry ExtendedEntryElement => Element as ExtendedEntry;

        /// <summary>
        /// При изменении элемента
        /// </summary>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control != null)
                {
                    Control.Style = Windows.UI.Xaml.Application.Current.Resources["FormTextBoxStyle"] as Windows.UI.Xaml.Style;
                }

                // Событие при загрузке
                Control.Loaded -= OnControlLoaded;
                Control.Loaded += OnControlLoaded;
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

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
            {
                Control.Loaded -= OnControlLoaded;
            }

            base.Dispose(disposing);
        }

        private void UpdateLineColor()
        {
            // Находим бордер
            var border = Control.FindVisualChildren<Border>()      
                                .Where(c => c.Name == "BorderElement")
                                .FirstOrDefault();

            if (border != null)
            {
                // Красим бордер
                border.BorderBrush = new SolidColorBrush(ExtendedEntryElement.LineColorToApply.ToUwp());
            }
        }

        /// <summary>
        /// При загрузке контрола
        /// </summary>
        private void OnControlLoaded(object sender, RoutedEventArgs e)
        {
            // Обновляем цвет границы
            UpdateLineColor();
        }
    }
}
