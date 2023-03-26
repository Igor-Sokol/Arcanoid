using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.DependencyInjection
{
    public class HealthServiceInstaller : MonoInstaller
    {
        [SerializeField] private HealthManager healthManager;

        public override void InstallBindings()
        {
            Container.Bind<IHealthManager>()
                .FromInstance(healthManager)
                .AsSingle()
                .NonLazy();
        }
    }
}