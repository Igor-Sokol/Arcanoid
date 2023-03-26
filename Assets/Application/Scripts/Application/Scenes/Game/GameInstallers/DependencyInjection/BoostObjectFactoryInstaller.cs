using Application.Scripts.Application.Scenes.Game.Pools.BoostObjectProviders.Factory;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.DependencyInjection
{
    public class BoostObjectFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBoostObjectFactory>()
                .To<BoostObjectFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}