using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Validations
{
    public class ValidatableObject<T> : BindableObject, IValidity
    {
        /// <summary>
        /// Значение
        /// </summary>
        private T _value;
        private bool _isValid;

        /// <summary>
        /// Правила валидации
        /// </summary>
        public List<IValidationRule<T>> Validations { get; }
        /// <summary>
        /// Ошибки
        /// </summary>
        public ObservableCollection<string> Errors { get; }

        public T Value
        {
            get => _value;

            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get => _isValid;

            set
            {
                _isValid = value;
                Errors.Clear();
                OnPropertyChanged();
            }
        }

        public ValidatableObject()
        {
            _isValid = true;
            Errors = new ObservableCollection<string>();
            Validations = new List<IValidationRule<T>>();
        }

        /// <summary>
        /// Проверка
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            // Очищаем имеющиеся ошибки
            Errors.Clear();

            // Проводим выборку на наличие новых ошибок
            var errors = Validations.Where(v => !v.Check(Value))
                                    .Select(v => v.ValidationMessage);

            // Проходим по ошибка заполняя коллекцию
            foreach (var error in errors)
            {
                Errors.Add(error);
            }

            // Если есть ошибки - НЕ валидная ситуация
            IsValid = !Errors.Any();

            return IsValid;
        }
    }
}
