using Application.Scripts.Application.Scenes.ChoosePack.LevelPacks.Factory;
using Zenject;

namespace Application.Scripts.Application.Scenes.ChoosePack.LevelPacks.DependencyInjection
{
    public class PackViewFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPackViewFactory>()
                .To<PackViewFactory>()
                .AsSingle();
        }
    }
}