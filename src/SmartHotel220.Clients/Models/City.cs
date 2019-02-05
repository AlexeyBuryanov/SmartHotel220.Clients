namespace SmartHotel220.Clients.Core.Models
{
    /// <summary>
    /// Город
    /// </summary>
    public class City
    {
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Country}";
        }
    }
}
