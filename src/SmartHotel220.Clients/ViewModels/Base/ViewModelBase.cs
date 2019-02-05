using SmartHotel220.Clients.Core.Services.Dialog;
using SmartHotel220.Clients.Core.Services.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.ViewModels.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Базовая модель представления
    /// </summary>
    public abstract class ViewModelBase : BindableObject
    {
        /// <summary>
        /// Занят или нет
        /// </summary>
        private bool _isBusy;

        /// <summary>
        /// Служба диалогов
        /// </summary>
        protected readonly IDialogService DialogService;
        /// <summary>
        /// Служба навигации
        /// </summary>
        protected readonly INavigationService NavigationService;

        protected ViewModelBase()
        {
            DialogService = Locator.Instance.Resolve<IDialogService>();
            NavigationService = Locator.Instance.Resolve<INavigationService>();
        }

        /// <summary>
        /// Занята ли активность св-во
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}