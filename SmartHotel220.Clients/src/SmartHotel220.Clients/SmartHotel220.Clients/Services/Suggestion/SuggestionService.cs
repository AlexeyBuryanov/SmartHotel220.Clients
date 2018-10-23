using SmartHotel220.Clients.Core.Extensions;
using SmartHotel220.Clients.Core.Services.Request;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Suggestion
{
    /// <inheritdoc />
    /// <summary>
    /// Служба предложений
    /// </summary>
    public class SuggestionService : ISuggestionService
    {
        /// <summary>
        /// Служба запросов
        /// </summary>
        private readonly IRequestService _requestService;

        public SuggestionService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        /// <summary>
        /// Получить список предложений на основе координат
        /// </summary>
        public async Task<ObservableCollection<Models.Suggestion>> GetSuggestionsAsync(double latitude, double longitude)
        {
            var builder = new UriBuilder(AppSettings.SuggestionsEndpoint);
            builder.AppendToPath("suggestions");
            builder.Query = $"latitude={latitude.ToString(CultureInfo.InvariantCulture)}&longitude={longitude.ToString(CultureInfo.InvariantCulture)}";

            var uri = builder.ToString();
            var suggestions = await _requestService.GetAsync<IEnumerable<Models.Suggestion>>(uri);

            return suggestions.ToObservableCollection();
        }
    }
}