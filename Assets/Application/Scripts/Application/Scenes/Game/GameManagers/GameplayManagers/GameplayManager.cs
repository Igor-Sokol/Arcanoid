using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Application.Scenes.Game.Units.Platform;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Level;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private BlockManager blockManager;
        [SerializeField] private BallsManager ballsManager;
        [SerializeField] private Platform platform;

        public void StartGame(LevelInfo levelInfo)
        {
            blockManager.PrepareReuse();
            ballsManager.PrepareReuse();

            blockManager.SetBlocks(levelInfo.LevelReader.ReadPack(levelInfo));
            platform.BallLauncher.SetBall(ballsManager.GetBall());
        }
    }
}