using Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Implementation;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.LibraryImplementations.DependencyInjection
{
    public class GameDependencyInstaller : MonoBehaviour, IInitializing
    {
        [SerializeField] private PoolBlockProvider blockProvider;
        
        public void Initialize()
        {
            ProjectContext.Instance.SetService<IBlockProvider, PoolBlockProvider>(blockProvider);
        }
    }
}