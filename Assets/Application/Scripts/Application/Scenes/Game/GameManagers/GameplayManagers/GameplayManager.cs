using Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostObjectManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.DifficultyManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.PopUpManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.ProcessManagers;
using Application.Scripts.Application.Scenes.Game.Units.Platform;
using Application.Scripts.Application.Scenes.Shared.Energy.Config;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers
{
    public class GameplayManager : MonoBehaviour
    {
        private IEnergyManager _energyManager;
        private IHealthManager _healthManager;

        [SerializeField] private EnergyValueConfig energyPriceConfig;
        [SerializeField] private DifficultyManager difficultyManager;
        [SerializeField] private BlockManager blockManager;
        [SerializeField] private BlockProgressManager blockProgressManager;
        [SerializeField] private GameProcessManager gameProcessManager;
        [SerializeField] private BallManager ballManager;
        [SerializeField] private ActiveBallManager activeBallManager;
        [SerializeField] private BoostManager boostManager;
        [SerializeField] private BoostObjectManager boostObjectManager;
        [SerializeField] private Platform platform;
        [SerializeField] private LoseManager loseManager;
        [SerializeField] private WinManager winManager;

        [Inject]
        private void Construct(IEnergyManager energyManager, IHealthManager healthManager)
        {
            _energyManager = energyManager;
            _healthManager = healthManager;
        }

        public void StartGame(LevelInfo levelInfo)
        {
            _energyManager.RemoveEnergy(energyPriceConfig.LevelPrice);
            gameProcessManager.PrepareReuse();
            winManager.PrepareReuse();
            _healthManager.PrepareReuse();
            blockManager.PrepareReuse();
            ballManager.PrepareReuse();
            boostObjectManager.PrepareReuse();
            boostManager.PrepareReuse();
            activeBallManager.PrepareReuse();

            blockManager.SetBlocks(levelInfo.LevelReader.ReadPack(levelInfo));
            blockProgressManager.PrepareReuse();
            difficultyManager.PrepareReuse();
            
            platform.BallLauncher.SetBall(ballManager.GetBall());
        }

        public void SetBall()
        {
            if (_healthManager.CurrentHealth > 0)
            {
                platform.BallLauncher.SetBall(ballManager.GetBall());
                _healthManager.RemoveHealth();
            }
            else
            {
                loseManager.PlayerLose();
            }
        }
    }
}