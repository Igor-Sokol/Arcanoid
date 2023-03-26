using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Contracts;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Implementations
{
    public class BoostActivator : DestroyService
    {
        private IBoostManager _boostManager;
        [SerializeField] private Boost[] boosts;

        [Inject]
        private void Construct(IBoostManager boostManager)
        {
            _boostManager = boostManager;
        }
        
        public override void Initialize()
        {
        }

        public override void PrepareReuse()
        {
        }

        public override void OnDestroyAction(Block block)
        {
            foreach (var boost in boosts)
            {
                boost.Configure(block);
            }
            
            _boostManager.Execute(boosts);
        }
    }
}