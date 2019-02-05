using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Helpers
{
    public class StatusBarHelper
    {
        private static readonly StatusBarHelper _instance = new StatusBarHelper();
        public const string TranslucentStatusChangeMessage = "TranslucentStatusChange";

        public static StatusBarHelper Instance => _instance;

        protected StatusBarHelper() {}

        /// <summary>
        /// Сделать статус-бар прозрачным
        /// </summary>
        public void MakeTranslucentStatusBar(bool translucent)
        {
            MessagingCenter.Send(this, TranslucentStatusChangeMessage, translucent);
        }
    }
}
