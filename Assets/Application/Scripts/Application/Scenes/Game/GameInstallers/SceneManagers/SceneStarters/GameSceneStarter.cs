using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.LevelPackManagers;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.SceneManagers.Contracts.Starter;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.SceneManagers.SceneStarters
{
    public class GameSceneStarter : SceneStarter, IInitializing
    {
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private LevelPackManager levelPackManager;

        public void Initialize()
        {
            StartScene();
        }
        
        public override void StartScene()
        {
            gameplayManager.StartGame(levelPackManager.GetCurrentLevel());
        }
    }
}