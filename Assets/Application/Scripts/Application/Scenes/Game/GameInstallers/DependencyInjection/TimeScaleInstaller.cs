using Application.Scripts.Library.TimeManagers;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.DependencyInjection
{
    public class TimeScaleInstaller : MonoInstaller
    {
        [SerializeField] private TimeScaleManager timeScaleManager;

        public override void InstallBindings()
        {
            Container.Bind<ITimeScaleManager>()
                .FromInstance(timeScaleManager)
                .AsSingle()
                .NonLazy();
        }
    }
}