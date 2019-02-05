using System;

namespace SmartHotel220.Clients.Core.Controls
{
    /// <inheritdoc />
    /// <summary>
    /// Аргументы события даты и времени, календарь
    /// </summary>
	public class DateTimeEventArgs : EventArgs
	{
		public DateTime DateTime { get; set; }
	}
}

