using Android.Content;
using SmartHotel220.Clients.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Slider), typeof(CustomSliderRenderer))]
namespace SmartHotel220.Clients.Droid.Renderers
{
    /// <summary>
    /// Рендер нашего кастомного слайдера
    /// </summary>
    public class CustomSliderRenderer : SliderRenderer
    {
        public CustomSliderRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            // Просто сбрасываем падинги
            Control?.SetPadding(0, 0, 0, 0);
        }
    }
}