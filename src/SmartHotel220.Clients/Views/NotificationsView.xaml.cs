using SmartHotel220.Clients.Core.Helpers;

namespace SmartHotel220.Clients.Core.Views
{
    public partial class NotificationsView
    {
        public NotificationsView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(false);
        }
    }
}