using System.ComponentModel.DataAnnotations;

namespace SmartHotel220.Clients.Core.Validations
{
    public class EmailRule : IValidationRule<string>
    {
        public EmailRule()
        {
            ValidationMessage = "Должен быть адрес электронной почты";
        }

        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return new EmailAddressAttribute().IsValid(value);
        }
    }
}
