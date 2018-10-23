namespace SmartHotel220.Clients.Core.Validations
{
    /// <summary>
    /// Правило валидации
    /// </summary>
    public interface IValidationRule<in T>
    {
        /// <summary>
        /// Сообщение
        /// </summary>
        string ValidationMessage { get; set; }

        /// <summary>
        /// Проверка значения
        /// </summary>
        bool Check(T value);
    }
}
