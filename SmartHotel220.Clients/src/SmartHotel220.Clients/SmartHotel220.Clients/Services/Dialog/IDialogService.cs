using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Dialog
{
    /// <summary>
    /// Описывает серси диалога
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Показать алерт-сообщение
        /// </summary>
        Task ShowAlertAsync(string message, string title, string buttonLabel);

        /// <summary>
        /// Показать тост
        /// </summary>
        void ShowToast(string message, int duration = 5000);

        /// <summary>
        /// Показать подтверждающий диалог
        /// </summary>
        Task<bool> ShowConfirmAsync(string message, string title, string okLabel, string cancelLabel);

        /// <summary>
        /// Показать диалог выбора
        /// </summary>
        Task<string> SelectActionAsync(string message, string title, IEnumerable<string> options);
        Task<string> SelectActionAsync(string message, string title, string cancelLabel, IEnumerable<string> options);
    }
}