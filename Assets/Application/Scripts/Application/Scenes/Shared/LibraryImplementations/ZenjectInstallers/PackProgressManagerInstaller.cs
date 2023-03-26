using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Contracts;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.ZenjectInstallers
{
    public class PackProgressManagerInstaller : MonoInstaller
    {
        [SerializeField] private PackProgressManager packProgressManager;

        public override void InstallBindings()
        {
            Container.Bind<IPackProgressManager>()
                .FromInstance(packProgressManager)
                .AsSingle()
                .NonLazy();
        }
    }
}