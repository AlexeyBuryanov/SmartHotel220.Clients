using SmartHotel220.Clients.Core.Services.Analytic;
using SmartHotel220.Clients.Core.Services.OpenUri;
using SmartHotel220.Clients.Core.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Мой номер
    /// </summary>
    public class MyRoomViewModel : ViewModelBase
    {
        // Строки мессенджеров
        private const string Skype = "Skype";
        private const string FacebookMessenger = "Facebook Messenger";
        private const string Telegram = "Telegram";

        /// <summary>
        /// Уровень света
        /// </summary>
        private double _ambientLight;
        /// <summary>
        /// Температура
        /// </summary>
        private double _temperature;
        /// <summary>
        /// Громкость музыки
        /// </summary>
        private double _musicVolume;
        /// <summary>
        /// Жалюзи
        /// </summary>
        private double _windowBlinds;
        /// <summary>
        /// Эко мод вкл или выкл
        /// </summary>
        private bool _isEcoMode;

        // Отображает какая вкладка сейчас активна
        private bool _ambient;
        private bool _need;
        private bool _find;
        private bool _noDisturb;

        /// <summary>
        /// Служба открытия ури
        /// </summary>
        private readonly IOpenUriService _openUrlService;
        /// <summary>
        /// Служба аналитики
        /// </summary>
        private readonly IAnalyticService _analyticService;

        public MyRoomViewModel(
            IOpenUriService openUrlService,
            IAnalyticService analyticService)
        {
            _openUrlService = openUrlService;
            _analyticService = analyticService;

            // Сразу делаем активной вкладку потребностей
            SetNeed();
        }

        /// <summary>
        /// Окружающий свет св-во
        /// </summary>
        public double AmbientLight
        {
            get => _ambientLight;
            set
            {
                _ambientLight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Температура св-во
        /// </summary>
        public double Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Громкость музыки св-во
        /// </summary>
        public double MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Жалюзи св-во
        /// </summary>
        public double WindowBlinds
        {
            get => _windowBlinds;
            set
            {
                _windowBlinds = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// ЭКО-мод св-во
        /// </summary>
        public bool IsEcoMode
        {
            get => _isEcoMode;
            set
            {
                _isEcoMode = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Окружение св-во
        /// </summary>
        public bool Ambient
        {
            get => _ambient;
            set
            {
                _ambient = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Требуется св-во
        /// </summary>
        public bool Need
        {
            get => _need;
            set
            {
                _need = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Найти св-во
        /// </summary>
        public bool Find
        {
            get => _find;
            set
            {
                _find = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Не беспокоить св-во
        /// </summary>
        public bool NoDisturb
        {
            get => _noDisturb;
            set
            {
                _noDisturb = value;
                OnPropertyChanged();
            }
        }

        public ICommand AmbientCommand => new Command(SetAmbient);
        public ICommand NeedCommand => new Command(SetNeed);
        public ICommand FindCommand => new Command(SetFind);
        public ICommand OpenDoorCommand => new AsyncCommand(OpenDoorAsync);
        public ICommand CheckoutCommand => new AsyncCommand(CheckoutAsync);
        public ICommand OpenBotCommand => new AsyncCommand(OpenBotAsync);
        public ICommand EcoModeCommand => new Command(EcoMode);

        /// <summary>
        /// Инициализация
        /// </summary>
        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            await Task.Delay(500);
            // Включаем режим по умолчанию
            ActivateDefaultMode();

            IsBusy = false;
        }

        private void SetAmbient()
        {
            Ambient = true;
            Need = false;
            Find = false;
        }
        private void SetNeed()
        {
            Ambient = false;
            Need = true;
            Find = false;
        }
        private void SetFind()
        {
            Ambient = false;
            Need = false;
            Find = true;
        }

        /// <summary>
        /// Открыть двери
        /// </summary>
        private Task OpenDoorAsync()
        {
            return NavigationService.NavigateToPopupAsync<OpenDoorViewModel>(true);
        }

        /// <summary>
        /// Выписка
        /// </summary>
        private Task CheckoutAsync()
        {
            return NavigationService.NavigateToPopupAsync<CheckoutViewModel>(true);
        }

        /// <summary>
        /// Открыть бот
        /// </summary>
        private async Task OpenBotAsync()
        {
            var bots = new[] { Skype, FacebookMessenger, Telegram };

            try
            {
                var selectedBot =
                    await DialogService.SelectActionAsync(
                        Resources.BotSelectionMessage,
                        Resources.BotSelectionTitle,
                        bots);

                switch (selectedBot)
                {
                    case Skype:
                        _openUrlService.OpenSkypeBot(AppSettings.SkypeBotId);
                        _analyticService.TrackEvent("SkypeBot");
                        break;
                    case FacebookMessenger:
                        _openUrlService.OpenFacebookBot(AppSettings.FacebookBotId);
                        _analyticService.TrackEvent("FacebookBot");
                        break;
                    case Telegram:
                        _openUrlService.OpenTelegramBot(AppSettings.TelegramBotId);
                        _analyticService.TrackEvent("TelegramBot");
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"OpenBot: {ex}");

                await DialogService.ShowAlertAsync(
                    Resources.BotError,
                    Resources.ExceptionTitle,
                    Resources.DialogOk);
            }
        }

        /// <summary>
        /// Переключить ЭКО-мод
        /// </summary>
        private void EcoMode()
        {
            if (IsEcoMode)
                ActivateDefaultMode(true);
            else
                ActivateEcoMode(true);
        }

        /// <summary>
        /// Активировать режим по дефолту
        /// </summary>
        private void ActivateDefaultMode(bool showToast = false)
        {
            IsEcoMode = false;

            AmbientLight = 3400;
            Temperature = 20;
            MusicVolume = 45;
            WindowBlinds = 80;

            if (showToast)
            {
                DialogService.ShowToast(Resources.DeactivateEcoMode, 1000);
            }
        }

        /// <summary>
        /// Активировать ЭКО-мод
        /// </summary>
        private void ActivateEcoMode(bool showToast = false)
        {
            IsEcoMode = true;

            AmbientLight = 2400;
            Temperature = 18;
            MusicVolume = 40;
            WindowBlinds = 50;

            if (showToast)
            {
                DialogService.ShowToast(Resources.ActivateEcoMode, 1000);
            }
        }
    }
}