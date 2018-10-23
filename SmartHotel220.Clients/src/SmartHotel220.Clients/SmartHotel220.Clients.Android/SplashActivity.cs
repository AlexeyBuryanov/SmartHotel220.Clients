using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;

namespace SmartHotel220.Clients.Droid
{
    /// <summary>
    /// Splash Screen
    /// </summary>
    [Activity(
        Label = "SmartHotel220",
        Icon = "@drawable/icon",
        Theme = "@style/SplashTheme",
        MainLauncher = true,
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Если SDK больше, чем Android 5.0
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                // Отображаем декорирующие UI элементы
                Window.DecorView.SystemUiVisibility = StatusBarVisibility.Visible;
                // Делаем статус-бар прозрачным
                Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
            }

            // Вызываем главную активность
            InvokeMainActivity();
        }

        private void InvokeMainActivity()
        {
            var mainActivityIntent = new Intent(this, typeof(MainActivity));
            StartActivity(mainActivityIntent);
        }
    }
}