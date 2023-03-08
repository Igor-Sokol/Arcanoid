using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.DependencyInjection.Contracts;
using Application.Scripts.Library.GameActionManagers;
using Application.Scripts.Library.GameActionManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.DependencyInjection
{
    public class GameActionManagerInstaller : ServiceInstaller
    {
        private ProjectContext _projectContext;

        [SerializeField] private GameActionManager gameActionManager;
        
        public override ProjectContext ProjectContext { get => _projectContext ??= ProjectContext.Instance; set => _projectContext = value; }
        public override void InstallService()
        {
            ProjectContext.Instance.SetService<IGameActionManager, GameActionManager>(gameActionManager);
        }
    }
}