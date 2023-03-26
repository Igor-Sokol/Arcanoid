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
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;
using Zenject;
using ProjectContext = Application.Scripts.Library.DependencyInjection.ProjectContext;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers
{
    public class GameplayManager : MonoBehaviour
    {
        private IEnergyManager _energyManager;

        [SerializeField] private EnergyValueConfig energyPriceConfig;
        [SerializeField] private DifficultyManager difficultyManager;
        [SerializeField] private HealthManager healthManager;
        [SerializeField] private BlockManager blockManager;
        [SerializeField] private BlockProgressManager blockProgressManager;
        [SerializeField] private GameProcessManager gameProcessManager;
        [SerializeField] private BallsManager ballsManager;
        [SerializeField] private ActiveBallManager activeBallManager;
        [SerializeField] private BoostManager boostManager;
        [SerializeField] private BoostObjectManager boostObjectManager;
        [SerializeField] private Platform platform;
        [SerializeField] private LoseManager loseManager;
        [SerializeField] private WinManager winManager;

        [Inject]
        private void Construct(IEnergyManager energyManager)
        {
            _energyManager = energyManager;
        }

        public void StartGame(LevelInfo levelInfo)
        {
            _energyManager.RemoveEnergy(energyPriceConfig.LevelPrice);
            gameProcessManager.PrepareReuse();
            winManager.PrepareReuse();
            healthManager.PrepareReuse();
            blockManager.PrepareReuse();
            ballsManager.PrepareReuse();
            boostObjectManager.PrepareReuse();
            boostManager.PrepareReuse();
            activeBallManager.PrepareReuse();

            blockManager.SetBlocks(levelInfo.LevelReader.ReadPack(levelInfo));
            blockProgressManager.PrepareReuse();
            difficultyManager.PrepareReuse();
            
            platform.BallLauncher.SetBall(ballsManager.GetBall());
        }

        public void SetBall()
        {
            if (healthManager.CurrentHealth > 0)
            {
                platform.BallLauncher.SetBall(ballsManager.GetBall());
                healthManager.RemoveHealth();
            }
            else
            {
                loseManager.PlayerLose();
            }
        }
    }
}