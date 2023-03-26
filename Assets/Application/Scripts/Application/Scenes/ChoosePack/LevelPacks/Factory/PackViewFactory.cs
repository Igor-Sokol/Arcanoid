using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.ChoosePack.LevelPacks.Factory
{
    public class PackViewFactory : IPackViewFactory
    {
        private readonly DiContainer _diContainer;
        
        public PackViewFactory(DiContainer container)
        {
            _diContainer = container;
        }
        
        public PackView Create(PackView prefab, Transform container)
        {
            return _diContainer.InstantiatePrefabForComponent<PackView>(prefab, container);
        }
    }
}