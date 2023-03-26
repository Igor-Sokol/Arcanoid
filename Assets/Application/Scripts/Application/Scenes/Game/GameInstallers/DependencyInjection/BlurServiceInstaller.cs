using Plugins.MobileBlur;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.DependencyInjection
{
    public class BlurServiceInstaller : MonoInstaller
    {
        [SerializeField] private MobileBlur mobileBlur;

        public override void InstallBindings()
        {
            Container.Bind<IBlur>()
                .FromInstance(mobileBlur)
                .AsSingle()
                .NonLazy();
        }
    }
}