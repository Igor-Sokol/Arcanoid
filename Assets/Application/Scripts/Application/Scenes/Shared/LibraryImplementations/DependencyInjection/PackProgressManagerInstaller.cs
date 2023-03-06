using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Contracts;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.DependencyInjection.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.DependencyInjection
{
    public class PackProgressManagerInstaller  : ServiceInstaller
    {
        private ProjectContext _projectContext;

        [SerializeField] private PackProgressManager packProgressManager;
        
        public override ProjectContext ProjectContext { get => _projectContext ??= ProjectContext.Instance; set => _projectContext = value; }
        public override void InstallService()
        {
            ProjectContext.Instance.SetService<IPackProgressManager, PackProgressManager>(packProgressManager);
        }
    }
}