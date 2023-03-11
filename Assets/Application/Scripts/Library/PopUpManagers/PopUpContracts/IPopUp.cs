using System;

namespace Application.Scripts.Library.PopUpManagers.PopUpContracts
{
    public interface IPopUp
    {
        bool Reserved { get; }
        bool Active { get; }
        event Action OnShown;
        event Action OnHidden;
        void Show();
        void Hide();
        void Reserve();
    }
}