using Application.Scripts.Application.Scenes.Shared.Energy;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.DependencyInjection.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.DependencyInjection
{
    public class EnergyManagerInstaller : ServiceInstaller
    {
        private ProjectContext _projectContext;

        [SerializeField] private EnergyManager energyManager;
        
        public override ProjectContext ProjectContext { get => _projectContext ??= ProjectContext.Instance; set => _projectContext = value; }
        public override void InstallService()
        {
            energyManager.Initialize();
            ProjectContext.Instance.SetService<IEnergyManager, EnergyManager>(energyManager);
        }
    }
}