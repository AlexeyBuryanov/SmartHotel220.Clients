using SmartHotel220.Clients.Core.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace SmartHotel220.Clients.Core.Services.Navigation
{
    /// <summary>
    /// Служба отвечающая за навигацию
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// Переместиться к
        /// </summary>
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        /// <summary>
        /// Переместиться к с параметром
        /// </summary>
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        /// <summary>
        /// Переместиться основываясь на тип модели представления
        /// </summary>
        Task NavigateToAsync(Type viewModelType);

        /// <summary>
        /// Переместиться основываясь на тип модели представления с параметром
        /// </summary>
        Task NavigateToAsync(Type viewModelType, object parameter);

        /// <summary>
        /// Назад
        /// </summary>
        Task NavigateBackAsync();

        /// <summary>
        /// Удалить последнее перемещение со стэка истории
        /// </summary>
        Task RemoveLastFromBackStackAsync();

        /// <summary>
        /// Перейти во всплывающее окно с анимацией или без
        /// </summary>
        Task NavigateToPopupAsync<TViewModel>(bool animate) where TViewModel : ViewModelBase;

        /// <summary>
        /// Перейти в сплывающее окно с анимацией или без и с параметром
        /// </summary>
        Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate) where TViewModel : ViewModelBase;
    }
}
