using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers.HealthConfig;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.DependencyInjection
{
    public class HealthServiceInstaller : MonoInstaller
    {
        [SerializeField] private HealthConfig healthConfig;

        public override void InstallBindings()
        {
            Container.Bind<IHealthConfig>().FromInstance(healthConfig);
            
            Container.Bind(typeof(IHealthManager), typeof(IInitializable))
                .To<HealthManager>()
                .AsSingle()
                .NonLazy();
        }
    }
}