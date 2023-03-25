using Application.Scripts.Library.PopUpManagers;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.ZenjectInstallers
{
    public class PopUpManagerInstaller : MonoInstaller
    {
        [SerializeField] private PopUpManager popUpManager;
        
        public override void InstallBindings()
        {
            Container.Bind<IPopUpManager>()
                .FromInstance(popUpManager)
                .AsSingle()
                .NonLazy();
        }
    }
}