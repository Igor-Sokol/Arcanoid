using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.ProcessManagers;
using Application.Scripts.Application.Scenes.Game.Units.Platform;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private HealthManager healthManager;
        [SerializeField] private BlockManager blockManager;
        [SerializeField] private BlockProgressManager blockProgressManager;
        [SerializeField] private BallsManager ballsManager;
        [SerializeField] private Platform platform;

        public void StartGame(LevelInfo levelInfo)
        {
            healthManager.PrepareReuse();
            blockManager.PrepareReuse();
            ballsManager.PrepareReuse();

            blockManager.SetBlocks(levelInfo.LevelReader.ReadPack(levelInfo));
            blockProgressManager.PrepareReuse();
            platform.BallLauncher.SetBall(ballsManager.GetBall());
        }
    }
}