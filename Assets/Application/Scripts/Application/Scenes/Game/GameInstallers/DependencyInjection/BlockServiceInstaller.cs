using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.DependencyInjection
{
    public class BlockServiceInstaller : MonoInstaller
    {
        [SerializeField] private BlockManager blockManager;

        public override void InstallBindings()
        {
            Container.Bind<IBlockManager>()
                .FromInstance(blockManager)
                .AsSingle()
                .NonLazy();
        }
    }
}