namespace SmartHotel220.Clients.Core.Models
{
    /// <summary>
    /// Заполненность
    /// </summary>
    public class Occupancy
    {
        /// <summary>
        /// Заполненность когда солнечно
        /// </summary>
        public double OcuppancyIfSunny { get; set; }
        /// <summary>
        /// Заполненность когда НЕ солнечно
        /// </summary>
        public double OccupancyIfNotSunny { get; set; }
    }
}
