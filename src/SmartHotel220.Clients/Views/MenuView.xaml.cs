using SmartHotel220.Clients.Core.ViewModels.Base;

namespace SmartHotel220.Clients.Core.Views
{
    public partial class MenuView
	{
		public MenuView()
		{
			InitializeComponent();
        }

        /// <summary>
        /// При появлении
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Если контекст реализовал наш интерфейс
            if (BindingContext is IHandleViewAppearing viewAware)
            {
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