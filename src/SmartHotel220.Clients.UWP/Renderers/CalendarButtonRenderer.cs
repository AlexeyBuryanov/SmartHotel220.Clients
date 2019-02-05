using SmartHotel220.Clients.Core.Controls;
using SmartHotel220.Clients.UWP.Renderers;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CalendarButton), typeof(CalendarButtonRenderer))]
namespace SmartHotel220.Clients.UWP.Renderers
{
    /// <summary>
    /// Рендер календарной кнопки
    /// </summary>
    public class CalendarButtonRenderer : ButtonRenderer
    {
        /// <summary>
        /// При изменении элемента
        /// </summary>
        protected override async void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control == null) return;

            // Задаём высоту и ширину
            Control.MinWidth = 48;
            Control.MinHeight = 48;

            // Рисуем фоновую картинку
            await DrawBackgroundImage();
        }

        /// <summary>
        /// При изменении св-ва привязки
        /// </summary>
        protected override async void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var element = Element as CalendarButton;

            if (Element.BorderWidth > 0 && (e.PropertyName == nameof(element.BorderWidth) || e.PropertyName == "Renderer"))
            {
                Control.BorderThickness = new Thickness(0);
            }
            if (e.PropertyName == nameof(element.BackgroundImage))
            {
                await DrawBackgroundImage();
            }
            if (e.PropertyName == nameof(element.Text) || e.PropertyName == nameof(element.TextWithoutMeasure))
            {
                await DrawBackgroundImage();
            }
        }

        private async Task DrawBackgroundImage()
        {
            // Кнопка
            var targetButton = Control;

            if (!(Element is CalendarButton sourceButton) || targetButton == null) return;

            // Если фоновая картинка есть
            if (sourceButton.BackgroundImage != null)
            {
                // Создаём сетку
                var grid = new Grid
                {
                    Padding = new Thickness(0)
                };

                // Получаем изображение
                var currentImage = await GetCurrentImage();

                // Устанавливаем выравнивание кнопке
                targetButton.HorizontalContentAlignment = HorizontalAlignment.Center;

                try
                {
                    // Устанавливаем выравнивание картинке
                    currentImage.HorizontalAlignment = HorizontalAlignment.Center;
                    // Добавляем картинку в сетку
                    grid.Children.Add(currentImage);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Ошибка настройки старого изображения {e}");
                }

                // Если кнопка не имеет текста
                if (!string.IsNullOrEmpty(sourceButton.Text) || !string.IsNullOrEmpty(sourceButton.Text))
                {
                    // Создаём label
                    var label = new TextBlock
                    {
                        TextAlignment = TextAlignment.Center,
                        FontSize = 16,
                        VerticalAlignment = VerticalAlignment.Center,
                        Text = string.IsNullOrEmpty(sourceButton.TextWithoutMeasure) 
                            ? sourceButton.Text 
                            : sourceButton.TextWithoutMeasure
                    };

                    // Добавляем в сетку
                    grid.Children.Add(label);
                }

                // Устанавливаем внутренний отступ
                targetButton.Padding = new Thickness(0);
                // Контент кнопки
                targetButton.Content = grid;
            }
            // Если нет фоновой картинки
            else if (sourceButton.BackgroundImage == null)
            {
                // Создаём сетку
                var grid = new Grid
                {
                    Padding = new Thickness(0)
                };

                // Создаём label
                var label = new TextBlock
                {
                    TextAlignment = TextAlignment.Center,
                    FontSize = 12,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = string.IsNullOrEmpty(sourceButton.TextWithoutMeasure) ? sourceButton.Text : sourceButton.TextWithoutMeasure
                };

                // Выравниваем
                targetButton.HorizontalContentAlignment = HorizontalAlignment.Center;
                // Добавляем label в сетку
                grid.Children.Add(label);

                targetButton.Padding = new Thickness(0);
                // Контент кнопки
                targetButton.Content = grid;
            }
        }

        /// <summary>
        /// Получить текущее изображение
        /// </summary>
        private Task<Image> GetCurrentImage()
        {
            if (!(Element is CalendarButton sourceButton)) return null;

            return GetImageAsync(sourceButton.BackgroundImage);
        }

        /// <summary>
        /// Обработчик изображения на основе источника
        /// </summary>
        private static IImageSourceHandler GetHandler(Xamarin.Forms.ImageSource source)
        {
            IImageSourceHandler returnValue = null;
            if (source is Xamarin.Forms.UriImageSource)
            {
                returnValue = new UriImageSourceHandler();
            }
            else if (source is Xamarin.Forms.FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (source is Xamarin.Forms.StreamImageSource)
            {
                returnValue = new StreamImageSourceHandler();
            }

            return returnValue;
        }

        /// <summary>
        /// Возвращает <see cref="Xamarin.Forms.Image" /> из предоставленного <see cref="ImageSource" />.
        /// </summary>
        /// <param name="source"><see cref="ImageSource" /> для загрузки изображения из.</param>
        /// <returns>Картинка с правильными размерами.</returns>
        private static async Task<Image> GetImageAsync(Xamarin.Forms.ImageSource source)
        {
            var image = new Image();
            var handler = GetHandler(source);

            var imageSource = await handler.LoadImageAsync(source);

            image.Source = imageSource;
            image.Stretch = Stretch.UniformToFill;
            image.VerticalAlignment = VerticalAlignment.Center;
            image.HorizontalAlignment = HorizontalAlignment.Center;

            return image;
        }
    }

    public static class Calendar
    {
        public static void Init()
        {
            var t1 = string.Empty;
        }
    }
}