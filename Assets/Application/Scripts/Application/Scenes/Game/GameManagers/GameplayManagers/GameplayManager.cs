using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostObjectManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.DifficultyManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.ProcessManagers;
using Application.Scripts.Application.Scenes.Game.Units.Platform;
using Application.Scripts.Application.Scenes.Shared.Energy.Config;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers
{
    public class GameplayManager : MonoBehaviour, IInitializing
    {
        private IEnergyManager _energyManager;

        [SerializeField] private EnergyValueConfig energyPriceConfig;
        [SerializeField] private DifficultyManager difficultyManager;
        [SerializeField] private HealthManager healthManager;
        [SerializeField] private BlockManager blockManager;
        [SerializeField] private BlockProgressManager blockProgressManager;
        [SerializeField] private BallsManager ballsManager;
        [FormerlySerializedAs("boostViewManager")] [SerializeField] private BoostObjectManager boostObjectManager;
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
            boostObjectManager.PrepareReuse();

            blockManager.SetBlocks(levelInfo.LevelReader.ReadPack(levelInfo));
            blockProgressManager.PrepareReuse();
            difficultyManager.PrepareReuse();
            
            SetBall();
        }

        public void SetBall()
        {
            platform.BallLauncher.SetBall(ballsManager.GetBall());
        }
    }
}