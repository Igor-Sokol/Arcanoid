using System;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Application.Scenes.Game.Units.Platform;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private HealthManager.HealthManager healthManager;
        [SerializeField] private BlockManager blockManager;
        [SerializeField] private BallsManager ballsManager;
        [SerializeField] private Platform platform;

        private void OnEnable()
        {
            ballsManager.OnAllBallRemoved += OnMissedBalls;
            healthManager.OnDead += OnPlayerLose;
        }

        private void OnDisable()
        {
            ballsManager.OnAllBallRemoved -= OnMissedBalls;
            healthManager.OnDead -= OnPlayerLose;
        }

        public void StartGame(IPackInfo packInfo)
        {
            blockManager.PrepareReuse();
            ballsManager.PrepareReuse();

            var level = packInfo.LevelPack.Levels.FirstOrDefault();
            blockManager.SetBlocks(level.LevelReader.ReadPack(level));
            platform.BallLauncher.SetBall(ballsManager.GetBall());
        }

        private void OnMissedBalls()
        {
            healthManager.RemoveHealth();

            if (healthManager.CurrentHealth > 0)
            {
                platform.BallLauncher.SetBall(ballsManager.GetBall());
            }
        }

        private void OnPlayerLose()
        {
            
        }

        private void OnPlayerWin()
        {
            
        }
    }
}