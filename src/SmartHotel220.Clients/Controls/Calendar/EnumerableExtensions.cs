using System;
using System.Collections.Generic;

namespace SmartHotel220.Clients.Core.Controls
{
    /// <summary>
    /// Расширение для IEnumerable. Метод ForEach
    /// </summary>
	public static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
		{
			foreach (var item in enumeration)
			{
				action(item);
			}
		}
	}
}