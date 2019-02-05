using Android.App;
using Android.Nfc.CardEmulators;
using Android.OS;
using SmartHotel220.Clients.Core;
using System;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Droid.Services.CardEmulation
{
    /// <inheritdoc />
    /// <summary>
    /// Эмулятор карты
    /// </summary>
    [Service(
        Exported = true, 
        Enabled = true, 
        Name = ServiceName,
        Permission = "android.permission.BIND_NFC_SERVICE"),
        IntentFilter(new[] { "android.nfc.cardemulation.action.HOST_APDU_SERVICE" }, 
        Categories = new[] { "android.intent.category.DEFAULT" }),
        MetaData("android.nfc.cardemulation.host_apdu_service",
        Resource = "@xml/apduservice")]
    public class CardService : HostApduService
    {
        public const string ServiceName = "smarthotel220.clients.droid.services.cardEmulation.cardService";

        // Статус "ОК" отправляется в ответ на команду SELECT AID (0x9000)
        private static readonly byte[] SELECT_OK_SW = HexStringToByteArray("9000");

        // Статус "UNKNOWN" отправляется в ответ на недопустимую команду APDU (0x0000)
        private static readonly byte[] UNKNOWN_CMD_SW = HexStringToByteArray("0000");

        private static string _messageValue;

        public CardService()
        {
            // Подписка на событие связанное с отправкой NFC-токена
            MessagingCenter.Subscribe<string>(this, MessengerKeys.SendNFCToken, StartNFCService);
        }

        /// <summary>
        /// Обработка команды Apdu
        /// </summary>
        public override byte[] ProcessCommandApdu(byte[] commandApdu, Bundle extras)
        {
            if (!string.IsNullOrEmpty(_messageValue))
            {
                return ConcatArrays(Encoding.UTF8.GetBytes(_messageValue), SELECT_OK_SW);
            }
            else
            {
                return UNKNOWN_CMD_SW;
            }
        }

        public override void OnDeactivated(DeactivationReason reason)
        {
        }

        /// <summary>
        /// Шестнадцатиричную строку в массив байт
        /// </summary>
        private static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                     .ToArray();
        }

        /// <summary>
        /// Объеденить/сцепить массивы
        /// </summary>
        private static byte[] ConcatArrays(byte[] first, byte[] rest)
        {
            var result = new byte[first.Length + rest.Length];
            first.CopyTo(result, 0);
            rest.CopyTo(result, first.Length);

            return result;
        }

        /// <summary>
        /// Старт службы
        /// </summary>
        private void StartNFCService(string message)
        {
            _messageValue = message;
        }
    }
}