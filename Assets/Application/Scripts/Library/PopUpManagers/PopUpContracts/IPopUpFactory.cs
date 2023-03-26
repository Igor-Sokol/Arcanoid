using UnityEngine;

namespace Application.Scripts.Library.PopUpManagers.PopUpContracts
{
    public interface IPopUpFactory
    {
        T Create<T>(T prefab, Transform container) where T : MonoBehaviour;
    }
}