using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Implementations;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.SceneManagers.Contracts.Starter;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.SceneManagers.SceneStarters
{
    public class GameSceneStarter : SceneStarter, IInitializing
    {
        private IPackInfo _sceneArgs;

        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private PackInfoMono defaultPackInfo;
        
        public void Initialize()
        {
            StartScene();
        }
        
        public override void StartScene()
        {
            var packInfo = ProjectContext.Instance.GetService<IPackInfo>();
            gameplayManager.StartGame(packInfo ?? defaultPackInfo);
        }
    }
}