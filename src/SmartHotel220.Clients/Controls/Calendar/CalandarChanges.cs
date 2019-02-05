using System;

namespace SmartHotel220.Clients.Core.Controls
{
    /// <summary>
    /// Изменения календаря
    /// </summary>
	[Flags]
	public enum CalandarChanges
	{
		MaxMin = 1,
        // Начальня дата
		StartDate = 1 << 1,
        // Начальный день
		StartDay = 1 << 2,
		All = MaxMin | StartDate | StartDay
	}
}