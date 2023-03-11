using System;

namespace Application.Scripts.Library.PopUpManagers.AnimationContracts
{
    public interface IPopUpAnimator
    {
        event Action OnAnimationShown;
        event Action OnAnimationHidden;
        void ShowAnimation();
        void HideAnimation();
    }
}