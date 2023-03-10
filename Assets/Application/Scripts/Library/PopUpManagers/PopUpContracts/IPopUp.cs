using System;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;

namespace Application.Scripts.Library.PopUpManagers.PopUpContracts
{
    public interface IPopUp
    {
        bool Active { get; }
        event Action OnShown;
        event Action OnHidden;
        void Show();
        void Hide();
    }
}