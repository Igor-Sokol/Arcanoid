using System;
using UnityEngine;

namespace Application.Scripts.Library.PopUpManagers.AnimationContracts
{
    public abstract class PopUpAnimator : MonoBehaviour, IPopUpAnimator
    {
        public abstract void ShowAnimation();
        public abstract void HideAnimation();
        public abstract event Action OnAnimationShown;
        public abstract event Action OnAnimationHidden;
    }
}