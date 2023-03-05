using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.DependencyInjection
{
    public class GameDependencyInstaller : MonoBehaviour, IInitializing
    {
        [SerializeField] private BlockManager blockManager;
        
        public void Initialize()
        {
            ProjectContext.Instance.SetService<BlockManager, BlockManager>(blockManager);
        }
    }
}