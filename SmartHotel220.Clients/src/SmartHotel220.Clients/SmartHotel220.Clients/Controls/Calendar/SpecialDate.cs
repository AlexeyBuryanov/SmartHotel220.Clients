using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    /// <summary>
    /// Специальная дата
    /// </summary>
	public class SpecialDate
	{
		public SpecialDate(DateTime date)
		{
			Date = date;
		}

        /// <summary>
        /// Дата
        /// </summary>
		public DateTime Date { get; set; }
        /// <summary>
        /// Цвет текста
        /// </summary>
		public Color? TextColor { get; set; }
        /// <summary>
        /// Цвет фона
        /// </summary>
		public Color? BackgroundColor { get; set; }
        /// <summary>
        /// Цвет бордера
        /// </summary>
		public Color? BorderColor { get; set; }
        /// <summary>
        /// Атрибуты шрифта
        /// </summary>
		public FontAttributes? FontAttributes { get; set; }
        /// <summary>
        /// Семейство шрифта
        /// </summary>
		public string FontFamily { get; set; }
        /// <summary>
        /// Ширина границы
        /// </summary>
		public int? BorderWidth { get; set; }
        /// <summary>
        /// Размер шрифта
        /// </summary>
		public double? FontSize { get; set; }
        /// <summary>
        /// Выбор
        /// </summary>
		public bool Selectable { get; set; }

        /// <summary>
        /// Получает или задает фоновое изображение (работает только на iOS и Android).
        /// </summary>
        /// <value>Фоновый рисунок.</value>
        public FileImageSource BackgroundImage { get; set; }

        /// <summary>
        /// Получает или задает фоновый рисунок (работает только на iOS и Android).
        /// </summary>
        /// <value>Фоновый рисунок.</value>
        public BackgroundPattern BackgroundPattern{ get; set; }
    } // SpecialDate

    /// <summary>
    /// Фоновый паттерн
    /// </summary>
    public class BackgroundPattern
	{
		protected int Columns;
	    public List<Pattern> Pattern;

        public BackgroundPattern(int columns)
		{
			Columns = columns;
		}

		public float GetTop(int t)
		{
			float r = 0;
			for (var i = t-Columns; i > -1; i-=Columns)
			{
				r += Pattern[i].HightPercent;
			}
			return r;
		}

		public float GetLeft(int l)
		{
			float r = 0;
			for (var i = l-1; i > -1 && (i+1) % Columns != 0; i--)
			{
				r += Pattern[i].WidthPercent;
			}
			return r;
		}
    } // BackgroundPattern

    public struct Pattern
	{
        /// <summary>
        /// Процент ширины
        /// </summary>
		public float WidthPercent;
        /// <summary>
        /// Процент высоты
        /// </summary>
		public float HightPercent;
        /// <summary>
        /// Цвет
        /// </summary>
		public Color Color;
	}
}
