using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.DependencyInjection.Contracts;
using Application.Scripts.Library.PopUpManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.DependencyInjection
{
    public class PopUpManagerInstaller : ServiceInstaller
    {
        private ProjectContext _projectContext;

        [SerializeField] private PopUpManager popUpManager;
        
        public override ProjectContext ProjectContext { get => _projectContext ??= ProjectContext.Instance; set => _projectContext = value; }
        public override void InstallService()
        {
            ProjectContext.Instance.SetService<IPopUpManager, PopUpManager>(popUpManager);
        }
    }
}