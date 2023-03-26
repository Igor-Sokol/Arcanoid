using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Factory
{
    public class BallFactory : IBallFactory
    {
        private readonly DiContainer _diContainer;
        
        public BallFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }


        public Ball Create(Ball prefab)
        {
            return _diContainer.InstantiatePrefabForComponent<Ball>(prefab);
        }
    }
}