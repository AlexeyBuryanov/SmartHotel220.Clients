using SmartHotel220.Clients.Core.Helpers;
using SmartHotel220.Clients.Core.ViewModels.Base;

namespace SmartHotel220.Clients.Core.Views
{
    public partial class HomeView
    {
        public HomeView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При появлении
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Делаем статус-бар прозрачным
            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);

            // Если контекст реализовал наш интерфейс
            if (BindingContext is IHandleViewAppearing viewAware)
            {
                // Вызываем ещё наш метод при появлении
                await viewAware.OnViewAppearingAsync(this);
            }
        }

        /// <summary>
        /// При исчезновении
        /// </summary>
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            // Если контекст реализовал наш интерфейс
            if (BindingContext is IHandleViewDisappearing viewAware)
            {
                await viewAware.OnViewDisappearingAsync(this);
            }
        }
    }
}