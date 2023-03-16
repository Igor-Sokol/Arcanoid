using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.DependencyInjection.Contracts;
using Application.Scripts.Library.InitializeManager;
using Application.Scripts.Library.SceneManagers;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.DependencyInjection
{
    public class SceneManagerInstaller : ServiceInstaller
    {
        private ProjectContext _projectContext;

        [SerializeField] private SceneManager sceneManager;
        [SerializeField] private Initializer loadingUIInitializer;
        
        public override ProjectContext ProjectContext { get => _projectContext ??= ProjectContext.Instance; set => _projectContext = value; }
        public override void InstallService()
        {
            ProjectContext.Instance.SetService<ISceneManager, SceneManager>(sceneManager);
            loadingUIInitializer.Initialize();
        }
    }
}