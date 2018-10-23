using SmartHotel220.Clients.Core.Services.Settings;
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
    /// Настройки
    /// </summary>
    public class SettingsViewModel<TRemoteSettingsModel> : ViewModelBase where TRemoteSettingsModel : class
    {
        // Служба настроек
        private readonly ISettingsService<TRemoteSettingsModel> _settingsService;

        // URL файла с настройками
        private ValidatableObject<string> _settingsFileUrl;
        // Удалённые настройки
        private TRemoteSettingsModel _remoteSettings;

        public SettingsViewModel(ISettingsService<TRemoteSettingsModel> settingsService)
        {
            _settingsService = settingsService;

            _settingsFileUrl = new ValidatableObject<string>();

            AddValidations();
        }

        /// <summary>
        /// URL файла с настройками св-во
        /// </summary>
        public ValidatableObject<string> SettingsFileUrl
        {
            get => _settingsFileUrl;
            set
            {
                _settingsFileUrl = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Удалённые настройки св-во
        /// </summary>
        public TRemoteSettingsModel RemoteSettings
        {
            get => _remoteSettings;
            set
            {
                _remoteSettings = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCommand => new AsyncCommand(UpdateSettingsAsync);

        /// <summary>
        /// Инициализация
        /// </summary>
        public override async Task InitializeAsync(object navigationData)
        {
            SettingsFileUrl.Value = _settingsService.RemoteFileUrl;
            RemoteSettings = await _settingsService.LoadSettingsAsync();
        }

        /// <summary>
        /// Добавить проверки
        /// </summary>
        private void AddValidations()
        {
            _settingsFileUrl.Validations.Add(new IsNotNullOrEmptyRule<string>());
            _settingsFileUrl.Validations.Add(new ValidUrlRule());
        }

        /// <summary>
        /// Проверить валидность
        /// </summary>
        private bool Validate()
        {
            return _settingsFileUrl.Validate();
        }

        /// <summary>
        /// Обновить настройки
        /// </summary>
        private async Task UpdateSettingsAsync(object obj)
        {
            try
            {
                IsBusy = true;

                // Если валидно
                if (Validate())
                {
                    // Загружаем удалённые настройки
                    RemoteSettings = await _settingsService.LoadRemoteSettingsAsync(_settingsFileUrl.Value);
                    // Сохраняем полученные настройки
                    await _settingsService.PersistRemoteSettingsAsync(RemoteSettings);
                    _settingsService.RemoteFileUrl = SettingsFileUrl.Value;

                    await DialogService.ShowAlertAsync("Удаленные настройки были успешно загружены", "Настройки JSON загружены!", "OK");
                }
            }
            catch (Exception ex)
            {
                await DialogService.ShowAlertAsync(ex.Message, "Ошибка при загрузке JSON настроек", "Accept");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}