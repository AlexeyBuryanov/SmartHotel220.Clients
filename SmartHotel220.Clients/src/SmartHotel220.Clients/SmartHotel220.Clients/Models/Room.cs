namespace SmartHotel220.Clients.Core.Models
{
    /// <summary>
    /// Номер
    /// </summary>
    public class Room
    {
        public int RoomId { get; set; }

        /// <summary>
        /// Название номера
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// Тип номера
        /// </summary>
        public int RoomType { get; set; }

        /// <summary>
        /// Кол-во
        /// </summary>
        public int Quantity { get; set; }
    }
}