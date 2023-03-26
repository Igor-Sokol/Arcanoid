using Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Factory;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.DependencyInjection
{
    public class BallFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBallFactory>()
                .To<BallFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}