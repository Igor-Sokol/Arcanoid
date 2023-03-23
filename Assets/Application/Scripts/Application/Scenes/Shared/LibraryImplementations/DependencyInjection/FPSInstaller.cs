using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.DependencyInjection.Contracts;
using CodeStage.AdvancedFPSCounter;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.DependencyInjection
{
    public class FPSInstaller : ServiceInstaller
    {
        [SerializeField] private AFPSCounter aFPSCounterPrefab;
        
        public override ProjectContext ProjectContext { get; set; }
        public override void InstallService()
        {
#if FPS_DEBUG
            var instance = Instantiate(aFPSCounterPrefab, transform);
            ProjectContext.SetService<AFPSCounter, AFPSCounter>(instance);
#endif
        }
    }
}