using Application.Scripts.Application.Scenes.Game.GameInstallers.LevelInstallers;
using Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Platform;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.GameStarter
{
    public class GameStarter : MonoBehaviour, IInitializing
    {
        [SerializeField] private LevelInstaller levelInstaller;
        [SerializeField] private BallProvider ballProvider;
        [SerializeField] private Platform platform;

        public void Initialize()
        {
            levelInstaller.LoadLevel(default);
            platform.BallLauncher.SetBall(ballProvider.GetBall());
        }
    }
}