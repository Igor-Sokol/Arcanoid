using CodeStage.AdvancedFPSCounter;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.ZenjectInstallers
{
    public class FPSInstaller : MonoInstaller
    {
        [SerializeField] private AFPSCounter aFPSCounterPrefab;
        [SerializeField] private Transform container;
        
        public override void InstallBindings()
        {
#if FPS_DEBUG
            var instance = Instantiate(aFPSCounterPrefab, container);
            Container.Bind<AFPSCounter>()
                .FromInstance(instance)
                .AsSingle()
                .NonLazy();
#endif
        }
    }
}