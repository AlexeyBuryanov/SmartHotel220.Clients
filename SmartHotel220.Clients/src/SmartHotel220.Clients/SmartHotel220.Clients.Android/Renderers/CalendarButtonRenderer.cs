using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using SmartHotel220.Clients.Core.Controls;
using SmartHotel220.Clients.Droid.Renderers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CalendarButton), typeof(CalendarButtonRenderer))]
namespace SmartHotel220.Clients.Droid.Renderers
{
    /// <summary>
    /// Рендер календарной кнопки
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CalendarButtonRenderer : ButtonRenderer
    {
        public CalendarButtonRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        /// При изменении элемента
        /// </summary>
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control == null) return;

            // При изменении текста
            Control.TextChanged += (sender, a) =>
            {
                var element = Element as CalendarButton;
                if (Control.Text == element?.TextWithoutMeasure 
                    || string.IsNullOrEmpty(Control.Text) 
                    && string.IsNullOrEmpty(element?.TextWithoutMeasure)) return;
                Control.Text = element?.TextWithoutMeasure;
            };
            Control.SetPadding(0, 0, 0, 0);
            Control.ViewTreeObserver.GlobalLayout += (sender, args) => ChangeBackgroundPattern();
        }

        /// <summary>
        /// При изменении св-ва привязки
        /// </summary>
        protected override async void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var element = Element as CalendarButton;

            // Текст
            if (e.PropertyName == nameof(element.TextWithoutMeasure) || e.PropertyName == "Renderer")
            {
                Control.Text = element?.TextWithoutMeasure;
            } // if

            // Цвет текста
            if (e.PropertyName == nameof(Element.TextColor) || e.PropertyName == "Renderer")
            {
                Control.SetTextColor(Element.TextColor.ToAndroid());
            } // if

            // Фон, цвет бордюра, ширина бордюра
            if (e.PropertyName == nameof(Element.BorderWidth) 
                || e.PropertyName == nameof(Element.BorderColor) 
                || e.PropertyName == nameof(Element.BackgroundColor) 
                || e.PropertyName == "Renderer")
            {
                // Если нет бэкграунд паттерна, то создаём
                if (element?.BackgroundPattern == null)
                {
                    // Если нет фоновой картинки, то устанавливаем
                    if (element?.BackgroundImage == null)
                    {
                        var drawable = new GradientDrawable();
                        drawable.SetShape(ShapeType.Rectangle);
                        drawable.SetStroke((int)Element.BorderWidth, Element.BorderColor.ToAndroid());
                        drawable.SetColor(Element.BackgroundColor.ToAndroid());
                        Control.SetBackground(drawable);
                    }
                    else
                    {
                        await ChangeBackgroundImageAsync();
                    } // if
                }
                else
                {
                    ChangeBackgroundPattern();
                } // if
            } // if

            if (e.PropertyName == nameof(element.BackgroundPattern))
            {
                ChangeBackgroundPattern();
            } // if

            if (e.PropertyName == nameof(element.BackgroundImage))
            {
                if (element?.BackgroundImage == null)
                {
                    var drawable = new GradientDrawable();
                    drawable.SetShape(ShapeType.Rectangle);
                    drawable.SetStroke((int)Element.BorderWidth, Element.BorderColor.ToAndroid());
                    drawable.SetColor(Element.BackgroundColor.ToAndroid());
                    Control.SetBackground(drawable);
                }
                else
                {
                    await ChangeBackgroundImageAsync();
                } // if
            } // if
        }

        /// <summary>
        /// Изменить фоновоую картинку
        /// </summary>
        protected async Task ChangeBackgroundImageAsync()
        {
            if (!(Element is CalendarButton element) 
                || element.BackgroundImage == null)
                return;

            var image = await GetBitmap(element.BackgroundImage);

            var d = new List<Drawable>
            {
#pragma warning disable 618
                new BitmapDrawable(image)
#pragma warning restore 618
            };

            var layer = new LayerDrawable(d.ToArray());
            layer.SetLayerInset(d.Count - 1, 0, 0, 0, 0);
            Control?.SetBackground(layer);
        }

        protected void ChangeBackgroundPattern()
        {
            if (!(Element is CalendarButton element) 
                || element.BackgroundPattern == null 
                || Control.Width == 0)
                return;

            var d = new List<Drawable>();
            for (var i = 0; i < element.BackgroundPattern.Pattern.Count; i++)
            {
                d.Add(new ColorDrawable(element.BackgroundPattern.Pattern[i].Color.ToAndroid()));
            }

            var drawable = new GradientDrawable();
            drawable.SetShape(ShapeType.Rectangle);
            drawable.SetStroke((int)Element.BorderWidth, Element.BorderColor.ToAndroid());
            drawable.SetColor(Android.Graphics.Color.Transparent);
            d.Add(drawable);

            var layer = new LayerDrawable(d.ToArray());
            for (var i = 0; i < element.BackgroundPattern.Pattern.Count; i++)
            {
                var l = (int)(Control.Width * element.BackgroundPattern.GetLeft(i));
                var t = (int)(Control.Height * element.BackgroundPattern.GetTop(i));
                var r = (int)(Control.Width * (1 - element.BackgroundPattern.Pattern[i].WidthPercent)) - l;
                var b = (int)(Control.Height * (1 - element.BackgroundPattern.Pattern[i].HightPercent)) - t;
                layer.SetLayerInset(i, l, t, r, b);
            }
            layer.SetLayerInset(d.Count - 1, 0, 0, 0, 0);
            Control.SetBackground(layer);
        }

        private Task<Bitmap> GetBitmap(ImageSource image)
        {
            var handler = new FileImageSourceHandler();
            return handler.LoadImageAsync(image, Control.Context);
        }
    }

    public static class Calendar
    {
        public static void Init()
        {
            var d = string.Empty;
        }
    }
}