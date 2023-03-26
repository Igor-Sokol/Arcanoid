using Application.Scripts.Library.GameActionManagers;
using Application.Scripts.Library.GameActionManagers.Contracts;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.ZenjectInstallers
{
    public class GameActionManagerInstaller : MonoInstaller
    {
        [SerializeField] private GameActionManager gameActionManager;

        public override void InstallBindings()
        {
            Container.Bind<IGameActionManager>()
                .FromInstance(gameActionManager)
                .AsSingle()
                .NonLazy();
        }
    }
}