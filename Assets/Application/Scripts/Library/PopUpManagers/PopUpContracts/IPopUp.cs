using System;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;

namespace Application.Scripts.Library.PopUpManagers.PopUpContracts
{
    public interface IPopUp
    {
        bool Active { get; }
        IPopUpAnimator PopUpAnimator { get; set; }
        event Action OnShown;
        event Action OnHidden;
        void Show();
        void Hide();
    }
}