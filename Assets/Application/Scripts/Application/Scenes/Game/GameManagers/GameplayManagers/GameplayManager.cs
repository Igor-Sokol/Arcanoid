using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.ProcessManagers;
using Application.Scripts.Application.Scenes.Game.Units.Platform;
using Application.Scripts.Application.Scenes.Shared.Energy.Config;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers
{
    public class GameplayManager : MonoBehaviour, IInitializing
    {
        private IEnergyManager _energyManager;

        [SerializeField] private EnergyPriceConfig energyPriceConfig;
        [SerializeField] private HealthManager healthManager;
        [SerializeField] private BlockManager blockManager;
        [SerializeField] private BlockProgressManager blockProgressManager;
        [SerializeField] private BallsManager ballsManager;
        [SerializeField] private Platform platform;

        public void Initialize()
        {
            _energyManager = ProjectContext.Instance.GetService<IEnergyManager>();
        }
        
        public void StartGame(LevelInfo levelInfo)
        {
            _energyManager.RemoveEnergy(energyPriceConfig.LevelPrice);
            healthManager.PrepareReuse();
            blockManager.PrepareReuse();
            ballsManager.PrepareReuse();

            blockManager.SetBlocks(levelInfo.LevelReader.ReadPack(levelInfo));
            blockProgressManager.PrepareReuse();
            
            SetBall();
        }

        public void SetBall()
        {
            platform.BallLauncher.SetBall(ballsManager.GetBall());
        }
    }
}