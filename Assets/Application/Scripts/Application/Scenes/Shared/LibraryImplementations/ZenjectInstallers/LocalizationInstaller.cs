using Application.Scripts.Library.Localization.LocalizationManagers;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.ZenjectInstallers
{
    public class LocalizationInstaller : MonoInstaller
    {
        [SerializeField] private LocalizationManager localizationManager;
        
        public override void InstallBindings()
        {
            localizationManager.LoadLanguage();
            Container.Bind<ILocalizationManager>()
                .FromInstance(localizationManager)
                .AsSingle()
                .NonLazy();
        }
    }
}