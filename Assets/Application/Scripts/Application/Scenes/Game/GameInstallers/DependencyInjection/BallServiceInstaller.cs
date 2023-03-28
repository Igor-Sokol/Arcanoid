using Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Contracts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.DependencyInjection
{
    public class BallServiceInstaller : MonoInstaller
    {
        [SerializeField] private BallManager ballManager;
        [SerializeField] private BallProvider ballProvider;
        [SerializeField] private ActiveBallManager activeBallManager;
        
        public override void InstallBindings()
        {
            Container.Bind<IBallManager>()
                .FromInstance(ballManager)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IBallProvider>()
                .FromInstance(ballProvider)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IActiveBallManager>()
                .FromInstance(activeBallManager)
                .AsSingle()
                .NonLazy();
        }
    }
}