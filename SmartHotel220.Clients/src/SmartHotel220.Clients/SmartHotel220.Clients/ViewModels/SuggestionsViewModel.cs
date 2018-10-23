using SmartHotel220.Clients.Core.Models;
using SmartHotel220.Clients.Core.Services.Location;
using SmartHotel220.Clients.Core.Services.Suggestion;
using SmartHotel220.Clients.Core.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Предложения
    /// </summary>
    public class SuggestionsViewModel : ViewModelBase
    {
        /// <summary>
        /// Список кастомных "штырьков"
        /// </summary>
        private ObservableCollection<CustomPin> _customPins;
        /// <summary>
        /// Список предложений
        /// </summary>
        private ObservableCollection<Suggestion> _suggestions;

        /// <summary>
        /// Служба предложений и рекламы
        /// </summary>
        private readonly ISuggestionService _suggestionService;
        /// <summary>
        /// Локационная служба
        /// </summary>
        private readonly ILocationService _locationService;

        public SuggestionsViewModel(ISuggestionService suggestionService, ILocationService locationService)
        {
            _suggestionService = suggestionService;
            _locationService = locationService;
        }

        /// <summary>
        /// Пины св-во
        /// </summary>
        public ObservableCollection<CustomPin> CustomPins
        {
            get => _customPins;
            set
            {
                _customPins = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Предложения св-во
        /// </summary>
        public ObservableCollection<Suggestion> Suggestions
        {
            get => _suggestions;
            set
            {
                _suggestions = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Инициализация 
        /// </summary>
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                IsBusy = true;

                // Получаем текущую позицию
                var location = await _locationService.GetPositionAsync();
                // Получаем предложения на основе позиции
                Suggestions = await _suggestionService.GetSuggestionsAsync(location.Latitude, location.Longitude);

                // Создаём и заполняем пины
                CustomPins = new ObservableCollection<CustomPin>();
                foreach (var suggestion in Suggestions)
                {
                    CustomPins.Add(new CustomPin
                    {
                        Label = suggestion.Name,
                        Position = new Xamarin.Forms.Maps.Position(suggestion.Latitude, suggestion.Longitude),
                        Type = suggestion.SuggestionType
                    });
                }
            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"[Предложения] Ошибка получения данных: {httpEx}");

                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    await DialogService.ShowAlertAsync( 
                        string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                        Resources.HttpRequestExceptionTitle,
                        Resources.DialogOk);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Предложения] Ошибка: {ex}");

                await DialogService.ShowAlertAsync(
                    Resources.ExceptionMessage,
                    Resources.ExceptionTitle,
                    Resources.DialogOk);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}