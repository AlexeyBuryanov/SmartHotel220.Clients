using Android.Views.InputMethods;
using Plugin.CurrentActivity;
using SmartHotel220.Clients.Core.Services.DismissKeyboard;
using SmartHotel220.Clients.Droid.Services.DismissKeyboard;

[assembly: Xamarin.Forms.Dependency(typeof(DismissKeyboardService))]
namespace SmartHotel220.Clients.Droid.Services.DismissKeyboard
{
    /// <inheritdoc />
    /// <summary>
    /// Служба для отмены клавиатуры
    /// </summary>
    public class DismissKeyboardService : IDismissKeyboardService
    {
        public void DismissKeyboard()
        {
            var inputMethodManager = InputMethodManager.FromContext(CrossCurrentActivity.Current.Activity.ApplicationContext);

            // Говорим, что нормально открытая клавиатура будет закрыто "мягко"
            inputMethodManager.HideSoftInputFromWindow(CrossCurrentActivity.Current.Activity.Window.DecorView.WindowToken, HideSoftInputFlags.NotAlways);
        }
    }
}