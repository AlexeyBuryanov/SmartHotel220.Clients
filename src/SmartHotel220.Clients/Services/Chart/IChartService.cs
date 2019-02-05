using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Chart
{
    /// <summary>
    /// Служба для работы с диаграммами
    /// </summary>
    public interface IChartService
    {
        Task<Microcharts.Chart> GetTemperatureChartAsync();
        Task<Microcharts.Chart> GetGreenChartAsync();
    }
}