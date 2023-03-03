using System;

namespace Application.Scripts.Library.PopUpManagers.AnimationContracts
{
    public interface IPopUpAnimator
    {
        void ShowAnimation();
        void HideAnimation();
        event Action OnAnimationShown;
        event Action OnAnimationHidden;
    }
}