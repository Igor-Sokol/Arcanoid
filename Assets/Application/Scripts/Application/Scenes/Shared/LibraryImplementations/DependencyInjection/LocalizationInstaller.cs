using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.DependencyInjection.Contracts;
using Application.Scripts.Library.Localization.LocalizationManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.DependencyInjection
{
    public class LocalizationInstaller : ServiceInstaller
    {
        private ProjectContext _projectContext;

        [SerializeField] private LocalizationManager localizationManager;
        
        public override ProjectContext ProjectContext { get => _projectContext ??= ProjectContext.Instance; set => _projectContext = value; }
        public override void InstallService()
        {
            localizationManager.LoadLanguage();
            ProjectContext.Instance.SetService<LocalizationManager, LocalizationManager>(localizationManager);
        }
    }
}