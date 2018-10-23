using SmartHotel220.Clients.Core.Helpers;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Views
{
    public partial class LoginView
    {
        public LoginView()
        {
            // Говорим, что данный элемент не имеет навигации
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
        }

        /// <summary>
        /// При появлении
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Делаем статус-бар прозрачным
            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
        }
    }
}