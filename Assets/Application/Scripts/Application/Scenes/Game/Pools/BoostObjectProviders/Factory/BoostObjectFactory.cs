using Application.Scripts.Application.Scenes.Game.Units.Boosts.Objects;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Pools.BoostObjectProviders.Factory
{
    public class BoostObjectFactory : IBoostObjectFactory
    {
        private readonly DiContainer _diContainer;
        
        public BoostObjectFactory(DiContainer container)
        {
            _diContainer = container;
        }
        
        public BoostObject Create(BoostObject prefab)
        {
            return _diContainer.InstantiatePrefabForComponent<BoostObject>(prefab);
        }
    }
}