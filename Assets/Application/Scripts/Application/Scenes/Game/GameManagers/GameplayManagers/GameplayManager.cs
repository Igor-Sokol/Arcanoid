using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Application.Scenes.Game.Units.Levels;
using Application.Scripts.Application.Scenes.Game.Units.Levels.Services.Readers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Platform;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private LevelReader levelReader;
        [SerializeField] private BlockManager blockManager;
        [SerializeField] private BallsManager ballsManager;
        [SerializeField] private Platform platform;

        public void StartGame(LevelInfo levelInfo)
        {
            blockManager.SetBlocks(levelReader.ReadPack(levelInfo));
            platform.BallLauncher.SetBall(ballsManager.GetBall());
        }
    }
}