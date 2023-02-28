using Application.Scripts.Library.PopUpManagers.AnimationContracts;

namespace Application.Scripts.Library.PopUpManagers.PopUpContracts
{
    public interface IPopUp
    {
        bool Active { get; }
        IPopUpAnimator PopUpAnimator { get; set; }
        void Show();
        void Hide();
    }
}