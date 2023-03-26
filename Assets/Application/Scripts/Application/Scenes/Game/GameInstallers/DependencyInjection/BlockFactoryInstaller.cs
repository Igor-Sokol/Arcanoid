using Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Factory;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.DependencyInjection
{
    public class BlockFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBlockFactory>()
                .To<BlockFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}