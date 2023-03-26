using Application.Scripts.Library.PopUpManagers.PopUpContracts;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.PopUpManagers
{
    public class ZenjectPopUpFactory : PopUpFactory
    {
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer container)
        {
            _diContainer = container;
        }
        
        public override T Create<T>(T prefab, Transform container)
        {
            return _diContainer.InstantiatePrefabForComponent<T>(prefab, container);
        }
    }
}