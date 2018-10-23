using SmartHotel220.Clients.Core.Models;
using SmartHotel220.Clients.Core.Services.Analytic;
using SmartHotel220.Clients.Core.Services.Authentication;
using SmartHotel220.Clients.Core.Validations;
using SmartHotel220.Clients.Core.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Вход
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        /// <summary>
        /// Служба аналитики
        /// </summary>
        private readonly IAnalyticService _analyticService;
        /// <summary>
        /// Служба аутентификации
        /// </summary>
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Валидатор имени пользователя
        /// </summary>
        private ValidatableObject<string> _userName;
        /// <summary>
        /// Валидатор пароля
        /// </summary>
        private ValidatableObject<string> _password;

        public LoginViewModel(
            IAnalyticService analyticService,
            IAuthenticationService authenticationService)
        {
            _analyticService = analyticService;
            _authenticationService = authenticationService;

            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            AddValidations();
        }

        /// <summary>
        /// Валидатор имени пользователя св-во
        /// </summary>
        public ValidatableObject<string> UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Валидатор пароля св-во
        /// </summary>
        public ValidatableObject<string> Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand SignInCommand => new AsyncCommand(SignInAsync);
        public ICommand MicrosoftSignInCommand => new AsyncCommand(MicrosoftSignInAsync);
        public ICommand SettingsCommand => new AsyncCommand(NavigateToSettingsAsync);

        /// <summary>
        /// Вход
        /// </summary>
        private async Task SignInAsync()
        {
            IsBusy = true;

            // Проверяем валидацию введённых данных
            var isValid = Validate();

            // Если всё ок
            if (isValid)
            {
                // Выполняем вход
                var isAuth = await _authenticationService.LoginAsync(UserName.Value, Password.Value);

                // Если успешно
                if (isAuth)
                {
                    IsBusy = false;

                    // Фиксируем вход в аналитике
                    _analyticService.TrackEvent("SignIn");
                    // Переходим на главную
                    await NavigationService.NavigateToAsync<MainViewModel>();
                }
            }

            IsBusy = false;
        }

        /// <summary>
        /// Вход через microsoft
        /// </summary>
        private async Task MicrosoftSignInAsync()
        {
            try
            {
                IsBusy = true;

                var succeeded = await _authenticationService.LoginWithMicrosoftAsync();

                if (succeeded)
                {
                    _analyticService.TrackEvent("MicrosoftSignIn");
                    await NavigationService.NavigateToAsync<MainViewModel>();
                }
            }
            catch (ServiceAuthenticationException)
            {
                await DialogService.ShowAlertAsync("Пожалуйста, повторите попытку", "Ошибка входа", "ОК");
            }
            catch (Exception)
            {
                await DialogService.ShowAlertAsync("Произошла ошибка, повторите попытку", "Ошибка", "ОК");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Добавить валидации
        /// </summary>
        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Имя пользователя не должно быть пустым" });
            _userName.Validations.Add(new EmailRule());
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Пароль не должен быть пустым" });
        }

        /// <summary>
        /// Проверка ввода
        /// </summary>
        private bool Validate()
        {
            var isValidUser = _userName.Validate();
            var isValidPassword = _password.Validate();

            return isValidUser && isValidPassword;
        }

        /// <summary>
        /// Перенаправление к странице настроек
        /// </summary>
        private Task NavigateToSettingsAsync(object obj)
        {
            return NavigationService.NavigateToAsync(typeof(SettingsViewModel<RemoteSettings>));
        }
    }
}