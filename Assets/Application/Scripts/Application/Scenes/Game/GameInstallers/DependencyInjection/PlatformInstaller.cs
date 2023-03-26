using Application.Scripts.Application.Scenes.Game.Units.Platform;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.DependencyInjection
{
    public class PlatformInstaller : MonoInstaller
    {
        [SerializeField] private Platform platform;

        public override void InstallBindings()
        {
            Container.Bind<Platform>()
                .FromInstance(platform)
                .AsSingle()
                .NonLazy();
        }
    }
}