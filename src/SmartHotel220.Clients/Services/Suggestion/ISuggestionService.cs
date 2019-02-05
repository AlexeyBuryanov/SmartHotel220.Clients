using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Suggestion
{
    /// <summary>
    /// Описывает службу предложений
    /// </summary>
    public interface ISuggestionService
    {
        /// <summary>
        /// Получить предложения по координатам
        /// </summary>
        Task<ObservableCollection<Models.Suggestion>> GetSuggestionsAsync(double latitude, double longitude);
    }
}