using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using UnityEngine;

namespace Application.Scripts.Library.PopUpManagers.PopUpContracts
{
    public abstract class PopUp : MonoBehaviour, IPopUp
    {
        public abstract bool Active { get; }
        public abstract IPopUpAnimator PopUpAnimator { get; set; }
        public abstract void Show();
        public abstract void Hide();
    }
}