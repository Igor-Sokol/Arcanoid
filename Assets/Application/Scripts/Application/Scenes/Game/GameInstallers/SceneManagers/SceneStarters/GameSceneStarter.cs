using Application.Scripts.Application.Scenes.Game.GameInstallers.SceneManagers.SceneInfo;
using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Level.ScriptableObjects;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.SceneManagers;
using Application.Scripts.Library.SceneManagers.Contracts.Starter;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.SceneManagers.SceneStarters
{
    public class GameSceneStarter : SceneStarter, IInitializing
    {
        private GameSceneArgs _sceneArgs;

        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private ScriptableLevelInfo defaultLevelInfo;
        
        public void Initialize()
        {
            StartScene();
        }
        
        public override void StartScene()
        {
            _sceneArgs = ProjectContext.Instance.GetService<SceneManager>().SceneArgs as GameSceneArgs;
            gameplayManager.StartGame(_sceneArgs?.LevelInfo ?? defaultLevelInfo.LevelInfo);
        }
    }
}