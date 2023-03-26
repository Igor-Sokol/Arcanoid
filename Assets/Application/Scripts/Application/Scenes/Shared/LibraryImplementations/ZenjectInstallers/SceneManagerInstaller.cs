using Application.Scripts.Library.InitializeManager;
using Application.Scripts.Library.SceneManagers;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.ZenjectInstallers
{
    public class SceneManagerInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private SceneManager sceneManager;
        [SerializeField] private Initializer loadingUIInitializer;
        
        public override void InstallBindings()
        {
            InstallSceneManager();
            InitializeSceneLoadings();
        }

        private void InstallSceneManager()
        {
            Container.Bind<ISceneManager>()
                .FromInstance(sceneManager)
                .AsSingle()
                .NonLazy();
        }
        
        private void InitializeSceneLoadings()
        {
            Container.Bind<IInitializable>()
                .To<SceneManagerInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        public void Initialize()
        {
            loadingUIInitializer.Initialize();
        }
    }
}