using SmartHotel220.Clients.Core.Views;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Utils
{
    /// <summary>
    /// Св-во привязки для навигационного бара (выезжающее меню слева(Navigation Drawer Menu))
    /// </summary>
    public static class NavigationBarAttachedProperty
    {
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.CreateAttached(
                "TextColor", 
                typeof(Color), 
                typeof(NavigationBarAttachedProperty), 
                Color.Default);

        public static Color GetTextColor(BindableObject view) => (Color)view.GetValue(TextColorProperty);

        public static void SetTextColor(BindableObject view, Color value)
        {
            view.SetValue(TextColorProperty, value);

            var page = view as Page;
            var parent = page?.Parent as CustomNavigationPage;
            parent?.ApplyNavigationTextColor(page);
        }
    }
}
