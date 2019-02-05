using SmartHotel220.Clients.Core.Controls;
using SmartHotel220.Clients.UWP.Renderers;
using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ButtonFrame), typeof(ButtonFrameRenderer))]
namespace SmartHotel220.Clients.UWP.Renderers
{
    /// <summary>
    /// Рендерер фрейма для кнопки
    /// </summary>
    public class ButtonFrameRenderer : FrameRenderer
    {
        /// <summary>
        /// Ширина тени
        /// </summary>
        private const int ShadowWidth = 1;
        private SpriteVisual _spriteVisual;

        /// <summary>
        /// При изменении элемента
        /// </summary>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
        {
            // Если есть новый элемент и созданный спрайт, то добавляем тень
            if (e.NewElement != null)
            {
                if (_spriteVisual == null)
                {
                    AddShadowChild();
                }
            }

            base.OnElementChanged(e);
        }

        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
        {
            _spriteVisual.Size = new Vector2((float)finalSize.Width + ShadowWidth, (float)finalSize.Height + ShadowWidth);

            return base.ArrangeOverride(finalSize);
        }

        /// <summary>
        /// Добавить тень дочернему элементу
        /// </summary>
        private void AddShadowChild()
        {
            var canvas = new Canvas();
            var compositor = ElementCompositionPreview.GetElementVisual(canvas).Compositor;
            _spriteVisual = compositor.CreateSpriteVisual();

            var dropShadow = compositor.CreateDropShadow();
            dropShadow.Offset = new Vector3(-ShadowWidth, -ShadowWidth, 0);
            dropShadow.Color = Colors.Black;
            dropShadow.Opacity = 0.6f;
            _spriteVisual.Shadow = dropShadow;

            ElementCompositionPreview.SetElementChildVisual(canvas, _spriteVisual);

            Children.Add(canvas);
        }
    }
}