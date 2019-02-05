using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    /// <summary>
    /// "Зелёная" диаграмма
    /// </summary>
    public class GreenChart : Chart
    {
        public GreenChart()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    BackgroundColor = SKColor.Parse("#F6F1E9");
                    GreenChartLabelTextSize = 14;
                    PointSize = 17;
                    break;
                case Device.UWP:
                    BackgroundColor = SKColor.Parse("#00F6F1E9");
                    GreenChartLabelTextSize = 9;
                    PointSize = 10;
                    break;
            }
        }

        /// <summary>
        /// Размер текста лэйбла
        /// </summary>
        public float GreenChartLabelTextSize { get; set; } = 14;

        /// <summary>
        /// Размер точки
        /// </summary>
        public float PointSize { get; set; } = 17;

        /// <summary>
        /// Режим точки
        /// </summary>
        public PointMode PointMode { get; set; } = PointMode.Circle;

        /// <summary>
        /// Диапазон значения
        /// </summary>
        private float ValueRange => MaxValue - MinValue;

        /// <summary>
        /// Посчитать начало Y
        /// </summary>
        /// <param name="itemHeight">Высота элемента</param>
        /// <param name="headerHeight">Высота заголовка</param>
        public float CalculateYOrigin(float itemHeight, float headerHeight)
        {
            // Если макс значение меньше либо равно 0, то это будет высота заголовка
            if (MaxValue <= 0)
            {
                return headerHeight;
            }

            // Если мин значение больше нуля, то это будет высота заголовка + высота айтема
            if (MinValue > 0)
            {
                return headerHeight + itemHeight;
            }

            // Иначе высота заголовка + макс значения / диапазон значений и * высоту элемента
            return headerHeight + MaxValue / ValueRange * itemHeight;
        }

        /// <summary>
        /// Рисуем контент
        /// </summary>
        public override void DrawContent(SKCanvas canvas, int width, int height)
        {
            // Размеры лэйблов значений
            var valueLabelSizes = MeasureValueLabels();
            // Высота подвала
            var footerHeight = CalculateFooterHeight(valueLabelSizes);
            // Высота заголовка
            var headerHeight = CalculateHeaderHeight(valueLabelSizes);
            // Размер элемента
            var itemSize = CalculateItemSize(width, height, footerHeight, headerHeight);
            // Начало
            var origin = CalculateYOrigin(itemSize.Height, headerHeight);
            // Точки
            var points = CalculatePoints(itemSize, origin, headerHeight);

            // Рисуем точечные области
            DrawPointAreas(canvas, points, origin);
            // Рисуем точки
            DrawPoints(canvas, points);
            // Рисуем подвал
            DrawFooter(canvas, points, itemSize, height, footerHeight);
        }

        /// <summary>
        /// Посчитать размер элемента
        /// </summary>
        protected SKSize CalculateItemSize(int width, int height, float footerHeight, float headerHeight)
        {
            // Всего
            var total = Entries.Count();
            // Ширина
            var w = (width - (total + 1) * Margin) / total;
            // Высота
            var h = height - Margin - footerHeight - headerHeight;

            return new SKSize(w, h);
        }

        /// <summary>
        /// Посчитать точки
        /// </summary>
        protected SKPoint[] CalculatePoints(SKSize itemSize, float origin, float headerHeight)
        {
            // Результат
            var result = new List<SKPoint>();

            // Проходим по записям диаграммы
            for (var i = 0; i < Entries.Count(); i++)
            {
                // Получаем запись
                var entry = Entries.ElementAt(i);

                // Подсчитываем x, y
                var x = Margin + (itemSize.Width / 2) + (i * (itemSize.Width + Margin));
                var y = headerHeight + (((MaxValue - entry.Value) / ValueRange) * itemSize.Height);

                var point = new SKPoint(x, y);
                result.Add(point);
            }

            return result.ToArray();
        }

        /// <summary>
        /// Рисуем подвал
        /// </summary>
        protected void DrawFooter(SKCanvas canvas, SKPoint[] points, SKSize itemSize, int height, float footerHeight)
        {
            DrawLabels(canvas, points, itemSize, height, footerHeight);
        }

        /// <summary>
        /// Рисуем лэйблы
        /// </summary>
        protected void DrawLabels(SKCanvas canvas, SKPoint[] points, SKSize itemSize, int height, float footerHeight)
        {
            // Проходим по записям диаграммы
            for (var i = 0; i < Entries.Count(); i++)
            {
                // Получаем запись
                var entry = Entries.ElementAt(i);
                var point = points[i];

                // Если лейбл не пустой
                if (!string.IsNullOrEmpty(entry.Label))
                {
                    using (var paint = new SKPaint())
                    {
                        // Настраиваем свойства рисования
                        paint.TextSize = GreenChartLabelTextSize;
                        paint.IsAntialias = true;
                        paint.Color = entry.TextColor;
                        paint.IsStroke = false;

                        // Отступы
                        var bounds = new SKRect();
                        // Текст
                        var text = entry.Label;
                        // Измеряем текст
                        paint.MeasureText(text, ref bounds);

                        // Рисуем текст
                        canvas.DrawText(text, point.X - (bounds.Width / 2), height - Margin + (GreenChartLabelTextSize / 2), paint);
                    }
                }
            }
        }

        /// <summary>
        /// Рисуем точки
        /// </summary>
        protected void DrawPoints(SKCanvas canvas, SKPoint[] points)
        {
            if (points.Length > 0 && PointMode != PointMode.None)
            {
                for (var i = 0; i < points.Length; i++)
                {
                    var entry = Entries.ElementAt(i);
                    var point = points[i];
                    canvas.DrawPoint(point, entry.Color, PointSize, PointMode);
                }
            }
        }

        /// <summary>
        /// Рисуем точечные области
        /// </summary>
        protected void DrawPointAreas(SKCanvas canvas, SKPoint[] points, float origin)
        {
            if (points.Length > 0)
            {
                for (var i = 0; i < points.Length; i++)
                {
                    var entry = Entries.ElementAt(i);
                    var point = points[i];
                    var y = Math.Min(origin, point.Y);

                    using (var paint = new SKPaint { Style = SKPaintStyle.Fill, Color = entry.Color })
                    {
                        var height = Math.Max(2, Math.Abs(origin - point.Y));
                        canvas.DrawRect(SKRect.Create(point.X - (PointSize / 2), y, PointSize, height), paint);
                    }
                }
            }
        }

        /// <summary>
        /// Посчитать высоту подвала
        /// </summary>
        protected float CalculateFooterHeight(SKRect[] valueLabelSizes)
        {
            var result = Margin;

            if (Entries.Any(e => !string.IsNullOrEmpty(e.Label)))
            {
                result += LabelTextSize - Margin;
            }

            return result;
        }

        /// <summary>
        /// Посчитать высоту заголовка
        /// </summary>
        protected float CalculateHeaderHeight(SKRect[] valueLabelSizes)
        {
            var result = Margin;

            if (Entries.Any())
            {
                var maxValueWidth = valueLabelSizes.Max(x => x.Width);

                if (maxValueWidth > 0)
                {
                    result += maxValueWidth + Margin;
                }
            }

            return result;
        }

        /// <summary>
        /// Измерить размеры лэйблов значений
        /// </summary>
        protected SKRect[] MeasureValueLabels()
        {
            using (var paint = new SKPaint())
            {
                paint.TextSize = LabelTextSize;

                // Выборка по записям
                return Entries.Select(e =>
                {
                    if (string.IsNullOrEmpty(e.ValueLabel))
                    {
                        return SKRect.Empty;
                    }

                    // Отступы
                    var bounds = new SKRect();
                    // Текст
                    var text = e.ValueLabel;
                    // Подсчитываем отступы
                    paint.MeasureText(text, ref bounds);

                    return bounds;
                }).ToArray();
            }
        }
    }
}