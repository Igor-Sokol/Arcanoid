using System;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using UnityEngine;

namespace Application.Scripts.Library.PopUpManagers.PopUpContracts
{
    public abstract class PopUp : MonoBehaviour, IPopUp
    {
        public abstract bool Active { get; }
        public abstract event Action OnShown;
        public abstract event Action OnHidden;
        public abstract void Show();
        public abstract void Hide();
    }
}