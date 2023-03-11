using Application.Scripts.Application.Scenes.Game.GameManagers.BoostObjectManagers;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Objects;
using Application.Scripts.Library.DependencyInjection;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Implementations
{
    public class BoostViewActivator : DestroyService
    {
        private IBoostObjectManager _boostObjectManager;
        
        [SerializeField] private BoostObject boostObject;

        public override void Initialize()
        {
            _boostObjectManager = ProjectContext.Instance.GetService<IBoostObjectManager>();
        }

        public override void PrepareReuse()
        {
        }

        public override void OnDestroyAction()
        {
            _boostObjectManager.GetBoostView(boostObject.Key).transform.position = transform.position;
        }
    }
}