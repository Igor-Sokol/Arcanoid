using System;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using UnityEngine;

namespace Application.Scripts.Library.PopUpManagers.PopUpContracts
{
    public abstract class PopUp : MonoBehaviour, IPopUp
    {
        public bool Reserved { get; private set; }
        public abstract bool Active { get; }
        public abstract event Action OnShown;
        public abstract event Action OnHidden;
        protected abstract void ShowAction();
        protected abstract void HideAction();
        public void Show()
        {
            transform.SetAsLastSibling();
            ShowAction();
        }

        public void Hide()
        {
            HideAction();
            Reserved = false;
        }

        public void Reserve()
        {
            Reserved = true;
        }
    }
}