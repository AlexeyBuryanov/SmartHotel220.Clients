using Android.Content;
using Android.Support.V7.Widget;
using SmartHotel220.Clients.Core.Views;
using SmartHotel220.Clients.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace SmartHotel220.Clients.Droid.Renderers
{
    /// <summary>
    /// Рендер нашей кастомной навигационной страницы
    /// </summary>
    public class CustomNavigationPageRenderer : NavigationPageRenderer
    {
        public CustomNavigationPageRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        /// Контроллер страницы, используется для платформенного рендеринга
        /// </summary>
        private IPageController PageController => Element;

        /// <summary>
        /// При разметке
        /// </summary>
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            // Высота контейнера
            var containerHeight = b - t;

            // Область контейнера
            PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

            // Добавление дочерних элементов
            for (var i = 0; i < ChildCount; i++)
            {
                var child = GetChildAt(i);

                // Toolbar не добавляем
                if (child is Toolbar)
                {
                    continue;
                }

                // Определяем размер и позицию вью
                child.Layout(0, 0, r, b);
            }
        }
    }
}