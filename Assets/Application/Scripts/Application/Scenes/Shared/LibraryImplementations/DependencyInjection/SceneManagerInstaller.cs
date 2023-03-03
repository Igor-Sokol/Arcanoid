using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.DependencyInjection.Contracts;
using Application.Scripts.Library.SceneManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.DependencyInjection
{
    public class SceneManagerInstaller : ServiceInstaller
    {
        private ProjectContext _projectContext;

        [SerializeField] private SceneManager sceneManager;
        
        public override ProjectContext ProjectContext { get => _projectContext ??= ProjectContext.Instance; set => _projectContext = value; }
        public override void InstallService()
        {
            ProjectContext.Instance.SetService<SceneManager, SceneManager>(sceneManager);
        }
    }
}