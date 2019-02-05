using SmartHotel220.Clients.Core.Helpers;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Views
{
    public partial class UwpSuggestionsView
    {
        private const double MinWidth = 720;

        public UwpSuggestionsView()
        {
            // Говорим, что данный элемент не имеет навигации
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent();

            // Центрируем карту
            MapHelper.CenterMapInDefaultLocation(MapControl);
            // Адаптируем разметку
            AdaptLayout();
        }

        /// <summary>
        /// При появлении
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            SizeChanged += OnSizeChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            SizeChanged -= OnSizeChanged;
        }

        /// <summary>
        /// При изменении размера окна
        /// </summary>
        private void OnSizeChanged(object sender, System.EventArgs e)
        {
            AdaptLayout();
        }

        /// <summary>
        /// Адаптация разметки
        /// </summary>
        private void AdaptLayout()
        {
            if (Width < 0)
            {
                return;
            }

            // Если окно разрешением меньше HD
            if (Width < MinWidth)
            {
                // Меняем расположение карты
                Grid.SetColumn(Map, 0);
                Grid.SetColumnSpan(Map, 2);
                // Отображаем список предложений по другому, более компактно
                LeftSuggestionList.IsVisible = false;
                BottomSuggestionList.IsVisible = true;
            }
            else
            {
                Grid.SetColumn(Map, 1);
                Grid.SetColumnSpan(Map, 1);
                LeftSuggestionList.IsVisible = true;
                BottomSuggestionList.IsVisible = false;
            }
        }
    }
}