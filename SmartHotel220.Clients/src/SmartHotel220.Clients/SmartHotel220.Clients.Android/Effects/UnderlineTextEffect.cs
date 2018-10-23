using Android.Widget;
using SmartHotel220.Clients.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(UnderlineTextEffect), "UnderlineTextEffect")]
namespace SmartHotel220.Clients.Droid.Effects
{
    /// <summary>
    /// Эффект подчёркивания
    /// </summary>
    public class UnderlineTextEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            // Если это label
            if (Control is TextView label)
            {
                label.PaintFlags |= Android.Graphics.PaintFlags.UnderlineText;
            }
        }

        protected override void OnDetached() { }
    }
}