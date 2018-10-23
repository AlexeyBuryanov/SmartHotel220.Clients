using Microsoft.AppCenter.Analytics;
using System.Collections.Generic;

namespace SmartHotel220.Clients.Core.Services.Analytic
{
    /// <summary>
    /// Служба аналитики AppCenter
    /// </summary>
    public class AnalyticService : IAnalyticService
    {
        /// <summary>
        /// Запечатлить событие
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="properties">Св-сва</param>
        public void TrackEvent(string name, Dictionary<string, string> properties = null)
        {
            Analytics.TrackEvent(name, properties);
        }
    }
}