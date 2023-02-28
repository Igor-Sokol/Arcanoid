using UnityEngine;

namespace Application.Scripts.Library.PopUpManagers.AnimationContracts
{
    public abstract class PopUpAnimator : MonoBehaviour, IPopUpAnimator
    {
        public abstract void ShowAnimation();
        public abstract void HideAnimation();
    }
}