using Android.App;
using Android.Nfc;
using Plugin.CurrentActivity;
using SmartHotel220.Clients.Core.Services.NFC;
using SmartHotel220.Clients.Droid.Services.NFC;

[assembly: Xamarin.Forms.Dependency(typeof(NfcService))]
namespace SmartHotel220.Clients.Droid.Services.NFC
{
    /// <summary>
    /// Наша служба NFC
    /// </summary>
    public class NfcService : INfcService
    {
        private readonly NfcAdapter _nfcDevice;

        public NfcService()
        {
            // Получаем активность
            var activity = CrossCurrentActivity.Current.Activity;
            // Получаем адаптер
            _nfcDevice = NfcAdapter.GetDefaultAdapter(activity);
        }

        /// <summary>
        /// Доступен ли
        /// </summary>
        public bool IsAvailable => _nfcDevice?.IsEnabled == true && _nfcDevice.IsNdefPushEnabled;
    }
}