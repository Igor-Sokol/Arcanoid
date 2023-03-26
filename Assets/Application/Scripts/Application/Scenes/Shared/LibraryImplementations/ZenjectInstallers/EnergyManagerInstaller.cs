using Application.Scripts.Application.Scenes.Shared.Energy;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.ZenjectInstallers
{
    public class EnergyManagerInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private EnergyManager energyManager;

        public override void InstallBindings()
        {
            InstallEnergyManager();
            InitializeEnergyManager();
        }

        private void InitializeEnergyManager()
        {
            Container.BindInterfacesTo<EnergyManagerInstaller>().FromInstance(this);
        }

        private void InstallEnergyManager()
        {
            Container.Bind<IEnergyManager>()
                .FromInstance(energyManager)
                .AsSingle()
                .NonLazy();
        }

        public void Initialize()
        {
            energyManager.Initialize();
        }
    }
}