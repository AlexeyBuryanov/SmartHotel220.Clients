using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Views.Templates
{
    public partial class UwpNotificationItemTemplate
    {
        public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create("TapCommand", typeof(ICommand), typeof(NotificationItemTemplate));

        public ICommand TapCommand
        {
            get => (ICommand)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }

        public UwpNotificationItemTemplate()
        {
            InitializeComponent();
        }
    }
}