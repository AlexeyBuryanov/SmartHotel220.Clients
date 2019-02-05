using Xamarin.Forms;
using SmartHotel220.Clients.Core.Helpers;

namespace SmartHotel220.Clients.Core.Views
{
    public partial class BookingHotelView
    {
        // Скорость параллас эффекта
        private const int ParallaxSpeed = 4;

        private double _lastScroll;

        public BookingHotelView()
        {
            // Говорим, что данный элемент не имеет навигации
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetBackButtonTitle(this, string.Empty);

            InitializeComponent();
        }

        /// <summary>
        /// При появлении
        /// </summary>
        protected override void OnAppearing()
        {
            // Отвязываем параллакс скролл
            base.OnDisappearing();

            // Делаем статус-бар прозрачным
            StatusBarHelper.Instance.MakeTranslucentStatusBar(false);

            // Привязываем параллакс скроллл
            ParallaxScroll.ParallaxView = HeaderView;
            ParallaxScroll.Scrolled += OnParallaxScrollScrolled;
        }

        /// <summary>
        /// При исчезновении
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ParallaxScroll.Scrolled -= OnParallaxScrollScrolled;
        }

        /// <summary>
        /// Когда параллакс скролл скроллится
        /// </summary>
        private void OnParallaxScrollScrolled(object sender, ScrolledEventArgs e)
        {
            double translation;

            // Если скролла не было, то сохраняем текущий
            if (_lastScroll == 0)
            {
                _lastScroll = e.ScrollY;
            }

            // Если последний скролл оказался больше текущего
            if (_lastScroll < e.ScrollY)
            {
                translation = 0 - ((e.ScrollY / ParallaxSpeed));

                if (translation > 0) translation = 0;
            }
            else
            {
                translation = 0 + ((e.ScrollY / ParallaxSpeed));

                if (translation > 0) translation = 0;
            }

            // Вызываем "затухание". Это и есть параллакс эффект
            SubHeaderView.FadeTo(translation < -40 ? 0 : 1);
   
            // Сохраняем последний скролл
            _lastScroll = e.ScrollY;
        }
    }
}