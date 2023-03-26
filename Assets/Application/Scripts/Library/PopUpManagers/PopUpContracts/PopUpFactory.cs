using UnityEngine;

namespace Application.Scripts.Library.PopUpManagers.PopUpContracts
{
    public abstract class PopUpFactory : MonoBehaviour, IPopUpFactory
    {
        public abstract T Create<T>(T prefab, Transform container) where T : MonoBehaviour;
    }
}