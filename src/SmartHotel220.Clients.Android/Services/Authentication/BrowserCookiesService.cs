using Android.OS;
using Android.Webkit;
using SmartHotel220.Clients.Core.Services.Authentication;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Droid.Services.Authentication
{
    /// <inheritdoc />
    /// <summary>
    /// Куки-сервис
    /// </summary>
    public class BrowserCookiesService : IBrowserCookiesService
    {
        /// <summary>
        /// Очистить куки
        /// </summary>
        public Task ClearCookiesAsync()
        {
            var context = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity;
            
            // Если SDK больше, чем Android 5.1.1
            if (Build.VERSION.SdkInt >= BuildVersionCodes.LollipopMr1)
            {
                System.Diagnostics.Debug.WriteLine("Очистка куки для API >= 5.1.1");
                CookieManager.Instance.RemoveAllCookies(null);
                CookieManager.Instance.Flush();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Очистка куки для API < 5.1.1");
#pragma warning disable 618
                var cookieSyncMngr = CookieSyncManager.CreateInstance(context);
                cookieSyncMngr.StartSync();
                var cookieManager = CookieManager.Instance;
                cookieManager.RemoveAllCookie();
                cookieManager.RemoveSessionCookie();
                cookieSyncMngr.StopSync();
                cookieSyncMngr.Sync();
#pragma warning restore 618
            }

            return Task.FromResult(true);
        }
    }
}