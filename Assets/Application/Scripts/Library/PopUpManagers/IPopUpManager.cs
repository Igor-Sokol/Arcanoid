using System.Collections.Generic;
using Application.Scripts.Library.PopUpManagers.PopUpContracts;
using UnityEngine;

namespace Application.Scripts.Library.PopUpManagers
{
    public interface IPopUpManager
    {
        IEnumerable<IPopUp> ActivePopUps { get; }
        T Show<T>() where T : MonoBehaviour, IPopUp;
    }
}