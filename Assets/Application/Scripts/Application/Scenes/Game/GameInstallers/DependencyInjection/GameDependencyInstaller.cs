using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostObjectManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Application.Scenes.Game.Units.Platform;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.TimeManagers;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.DependencyInjection
{
    public class GameDependencyInstaller : MonoBehaviour, IInitializing
    {
        [SerializeField] private Platform platform;
        [SerializeField] private BlockManager blockManager;
        [SerializeField] private BoostObjectManager boostObjectManager;
        [SerializeField] private BoostManager boostManager;
        [SerializeField] private HealthManager healthManager;
        [SerializeField] private TimeScaleManager timeScaleManager;
        [SerializeField] private BallsManager ballManager;
        
        public void Initialize()
        {
            ProjectContext.Instance.SetService<Platform, Platform>(platform);
            ProjectContext.Instance.SetService<IBlockManager, BlockManager>(blockManager);
            ProjectContext.Instance.SetService<IBoostObjectManager, BoostObjectManager>(boostObjectManager);
            ProjectContext.Instance.SetService<IBoostManager, BoostManager>(boostManager);
            ProjectContext.Instance.SetService<IHealthManager, HealthManager>(healthManager);
            ProjectContext.Instance.SetService<ITimeScaleManager, TimeScaleManager>(timeScaleManager);
            ProjectContext.Instance.SetService<IBallManager, BallsManager>(ballManager);
        }
    }
}