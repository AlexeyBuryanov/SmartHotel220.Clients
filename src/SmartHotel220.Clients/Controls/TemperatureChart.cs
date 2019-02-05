using Microcharts;
using SkiaSharp;
using System;
using System.Linq;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace SmartHotel220.Clients.Core.Controls
{
    /// <summary>
    /// Диаграмма температуры
    /// </summary>
    public class TemperatureChart : Chart
    {
        public TemperatureChart()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    BackgroundColor = SKColor.Parse("#F6F1E9");
                    LabelTextSize = 20;
                    CaptionMargin = 15;
                    LineSize = 34;
                    break;
                case Device.UWP:
                    BackgroundColor = SKColor.Parse("#00F6F1E9");
                    LabelTextSize = 10;
                    CaptionMargin = 9;
                    LineSize = 15;
                    break;
            }
        }

        /// <summary>
        /// Отступ заголовка
        /// </summary>
        public float CaptionMargin { get; set; } = 15;

        /// <summary>
        /// Размер линии
        /// </summary>
        public float LineSize { get; set; } = 34;

        /// <summary>
        /// Начальный угол
        /// </summary>
        public float StartAngle { get; set; } = -180;

        /// <summary>
        /// Абсолютный минимум
        /// </summary>
        private float AbsoluteMinimum => Entries.Select(x => x.Value).Concat(new[]
        {
            MaxValue, MinValue, InternalMinValue ?? 0
        }).Min(x => Math.Abs(x));

        /// <summary>
        /// Абсолютный максимум
        /// </summary>
        private float AbsoluteMaximum => Entries.Select(x => x.Value).Concat(new[]
        {
            MaxValue, MinValue, InternalMinValue ?? 0
        }).Max(x => Math.Abs(x));

        /// <summary>
        /// Диапазон значения
        /// </summary>
        private float ValueRange => AbsoluteMaximum - AbsoluteMinimum;

        public override void DrawContent(SKCanvas canvas, int width, int height)
        {
            var sumValue = Entries.Sum(x => Math.Abs(x.Value));
            var radius = (Math.Min(width, height) - (2 * Margin)) / 2;
            var cx = width / 2;
            var cy = Convert.ToInt32(height / 1.25);
            var lineWidth = (LineSize < 0) ? (radius / ((Entries.Count() + 1) * 2)) : LineSize;
            var radiusSpace = lineWidth * 4;

            foreach (var entry in Entries.OrderByDescending(e => e.Value))
            {
                DrawChart(canvas, entry, radiusSpace, cx, cy, lineWidth);
            }

            DrawCaption(canvas, cx, cy, radiusSpace);
        }

        public void DrawChart(SKCanvas canvas, Entry entry, float radius, int cx, int cy, float strokeWidth)
        {
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = strokeWidth,
                StrokeCap = SKStrokeCap.Round,
                Color = entry.Color,
                IsAntialias = true
            })
            {
                using (var path = new SKPath())
                {
                    var sweepAngle = 180 * (Math.Abs(entry.Value) - AbsoluteMinimum) / ValueRange;
                    path.AddArc(SKRect.Create(cx - radius, cy - radius, 2 * radius, 2 * radius), StartAngle, sweepAngle);
                    canvas.DrawPath(path, paint);
                }
            }
        }

        private void DrawCaption(SKCanvas canvas, int cx, int cy, float radius)
        {
            const int minimum = 0;
            var medium = Math.Round(Entries.Max(e => e.Value) / 2);
            var maximum = Entries.Max(e => e.Value);

            canvas.DrawCaptionLabels(string.Empty, SKColor.Empty, $"{minimum}°C", SKColors.Black, 
                LabelTextSize, new SKPoint(cx - radius - LineSize - CaptionMargin, cy), SKTextAlign.Center);
            canvas.DrawCaptionLabels(string.Empty, SKColor.Empty, $"{medium}°C", SKColors.Black, 
                LabelTextSize, new SKPoint(cx, cy - radius - LineSize), SKTextAlign.Center);
            canvas.DrawCaptionLabels(string.Empty, SKColor.Empty, $"{maximum}°C", SKColors.Black, 
                LabelTextSize, new SKPoint(cx + radius + LineSize + CaptionMargin, cy), SKTextAlign.Center);
        }
    }
}