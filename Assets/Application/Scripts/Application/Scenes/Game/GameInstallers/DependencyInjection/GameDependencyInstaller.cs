using Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostObjectManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Screen.Effects.EnvironmentShakers;
using Application.Scripts.Application.Scenes.Game.Units.Platform;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.TimeManagers;
using Application.Scripts.Library.TimeManagers.Contracts;
using Plugins.MobileBlur;
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
        [SerializeField] private BallProvider ballProvider;
        [SerializeField] private ActiveBallManager activeBallManager;
        [SerializeField] private MobileBlur mobileBlur;
        [SerializeField] private EnvironmentShake environmentShake;
        
        public void Initialize()
        {
            ProjectContext.Instance.SetService<Platform, Platform>(platform);
            ProjectContext.Instance.SetService<IBlockManager, BlockManager>(blockManager);
            ProjectContext.Instance.SetService<IBoostObjectManager, BoostObjectManager>(boostObjectManager);
            ProjectContext.Instance.SetService<IBoostManager, BoostManager>(boostManager);
            ProjectContext.Instance.SetService<IHealthManager, HealthManager>(healthManager);
            ProjectContext.Instance.SetService<ITimeScaleManager, TimeScaleManager>(timeScaleManager);
            ProjectContext.Instance.SetService<IBallManager, BallsManager>(ballManager);
            ProjectContext.Instance.SetService<IBallProvider, BallProvider>(ballProvider);
            ProjectContext.Instance.SetService<IActiveBallManager, ActiveBallManager>(activeBallManager);
            ProjectContext.Instance.SetService<IBlur, MobileBlur>(mobileBlur);
            ProjectContext.Instance.SetService<IEnvironmentShake, EnvironmentShake>(environmentShake);
        }
    }
}