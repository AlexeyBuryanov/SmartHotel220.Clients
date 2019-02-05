using SmartHotel220.Clients.Core.Utils;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Views
{
    public partial class CustomNavigationPage
    {
        public CustomNavigationPage()
        {
            InitializeComponent();
        }

        public CustomNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }

        internal void ApplyNavigationTextColor(Page targetPage)
        {
            // Цвет текста навигации
            var color = NavigationBarAttachedProperty.GetTextColor(targetPage);
            BarTextColor = color == Color.Default
                ? Color.White
                : color;
        }
    }
}