using SmartHotel220.Clients.Core.Services.DismissKeyboard;
using SmartHotel220.Clients.UWP.Services.DismissKeyboard;
using Windows.UI.ViewManagement;

[assembly: Xamarin.Forms.Dependency(typeof(DismissKeyboardService))]
namespace SmartHotel220.Clients.UWP.Services.DismissKeyboard
{
    public class DismissKeyboardService : IDismissKeyboardService
    {
        /// <summary>
        /// Попытка закрыть клавиатуру ввода, если она отображена
        /// </summary>
        public void DismissKeyboard()
        {
            InputPane.GetForCurrentView().TryHide();
        }
    }
}