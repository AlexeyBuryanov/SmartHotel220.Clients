using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.ViewModels.Base
{
    /// <summary>
    /// При появлении
    /// </summary>
    public interface IHandleViewAppearing
    {
        Task OnViewAppearingAsync(VisualElement view);
    }

    /// <summary>
    /// При исчезновении
    /// </summary>
    public interface IHandleViewDisappearing
    {
        Task OnViewDisappearingAsync(VisualElement view);
    }
}