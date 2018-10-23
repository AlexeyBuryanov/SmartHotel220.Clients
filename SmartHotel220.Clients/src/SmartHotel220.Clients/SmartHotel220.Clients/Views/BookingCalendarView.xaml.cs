using SmartHotel220.Clients.Core.Helpers;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Views
{
    public partial class BookingCalendarView
	{
		public BookingCalendarView ()
        {
            // Говорим, что данный элемент не имеет навигации
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent ();
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