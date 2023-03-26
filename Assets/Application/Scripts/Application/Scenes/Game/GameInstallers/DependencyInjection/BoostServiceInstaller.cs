using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostObjectManagers;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.DependencyInjection
{
    public class BoostServiceInstaller : MonoInstaller
    {
        [SerializeField] private BoostObjectManager boostObjectManager;
        [SerializeField] private BoostManager boostManager;

        public override void InstallBindings()
        {
            Container.Bind<IBoostObjectManager>()
                .FromInstance(boostObjectManager)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<IBoostManager>()
                .FromInstance(boostManager)
                .AsSingle()
                .NonLazy();
        }
    }
}