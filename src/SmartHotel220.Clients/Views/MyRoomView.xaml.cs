using SmartHotel220.Clients.Core.Helpers;

namespace SmartHotel220.Clients.Core.Views
{
    public partial class MyRoomView
    {
        public MyRoomView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При появлении
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Делаем статус-бар прозрачным
            StatusBarHelper.Instance.MakeTranslucentStatusBar(false);

            SizeChanged += OnSizeChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            SizeChanged -= OnSizeChanged;
        }

        private void OnSizeChanged(object sender, System.EventArgs e)
        {
            AmbientLightSlider.WidthRequest = Width;
        }
    }
}