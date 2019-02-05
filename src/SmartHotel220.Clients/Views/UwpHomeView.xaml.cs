using SmartHotel220.Clients.Core.ViewModels.Base;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Views
{
    public partial class UwpHomeView
    {
        private const double MinWidth = 720;

        public UwpHomeView()
        {
            InitializeComponent();

            // Адаптировать разметку
            AdaptLayout();
        }

        /// <summary>
        /// При появлении
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Если контекст реализовал наш интерфейс "при появлении"
            if (BindingContext is IHandleViewAppearing viewAware)
            {
                await viewAware.OnViewAppearingAsync(this);
            }

            SizeChanged += OnSizeChanged;
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            // Если контекст реализовал наш интерфейс "при исчезновении"
            if (BindingContext is IHandleViewDisappearing viewAware)
            {
                await viewAware.OnViewDisappearingAsync(this);
            }

            SizeChanged -= OnSizeChanged;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            AdaptLayout();
        }

        /// <summary>
        /// При изменении размера
        /// </summary>
        private void OnSizeChanged(object sender, System.EventArgs e)
        {
            AdaptLayout();
        }

        /// <summary>
        /// Адаптировать разметку
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
                // Ресторан и доп. инфу не показываем
                Grid.SetColumnSpan(RestaurantTitle, 2);
                Grid.SetColumnSpan(RestaurantContent, 2);
                RestaurantImage.IsVisible = false;
                MoreInfoTitle.IsVisible = false;
                MoreInfoContent.IsVisible = false;
                Grid.SetColumn(MoreInfoImage, 0);
                Grid.SetColumnSpan(MoreInfoImage, 2);
                // Переключаемся с больших диаграмм на маленькие
                BigCharts.IsVisible = false;
                SmallCharts.IsVisible = true;
            }
            // Если наоборот размера окна больше
            else
            {
                Grid.SetColumnSpan(RestaurantTitle, 1);
                Grid.SetColumnSpan(RestaurantContent, 1);
                RestaurantImage.IsVisible = true;
                MoreInfoTitle.IsVisible = true;
                MoreInfoContent.IsVisible = true;
                Grid.SetColumn(MoreInfoImage, 1);
                Grid.SetColumnSpan(MoreInfoImage, 1);
                BigCharts.IsVisible = true;
                SmallCharts.IsVisible = false;
            }
        }
    }
}