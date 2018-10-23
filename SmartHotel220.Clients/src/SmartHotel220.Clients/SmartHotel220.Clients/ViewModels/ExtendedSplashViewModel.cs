using SmartHotel220.Clients.Core.ViewModels.Base;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Splash screen
    /// </summary>
    public class ExtendedSplashViewModel : ViewModelBase
    {
        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            await NavigationService.InitializeAsync();

            IsBusy = false;
        }
    }
}
