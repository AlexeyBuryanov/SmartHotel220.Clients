using SmartHotel220.Clients.Core.Helpers;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Views
{
    public partial class SuggestionsView
    {
        public SuggestionsView()
        {
            // Говорим, что данный элемент не имеет навигации
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent();

            // Центрируем карту
            MapHelper.CenterMapInDefaultLocation(this.Map);
        }

        /// <summary>
        /// При появлении
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Прозрачный статус-бар
            StatusBarHelper.Instance.MakeTranslucentStatusBar(false);
        }
    }
}