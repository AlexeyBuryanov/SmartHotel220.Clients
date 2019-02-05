using SmartHotel220.Clients.UWP.Renderers;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Slider), typeof(CustomSliderRenderer))]
namespace SmartHotel220.Clients.UWP.Renderers
{
    /// <summary>
    /// Рендер слайдера
    /// </summary>
    public class CustomSliderRenderer : SliderRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Slider> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // Отключаем подсказку
                Control.IsThumbToolTipEnabled = false;
            }
        }
    }
}