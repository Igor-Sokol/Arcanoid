using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Contracts;
using Application.Scripts.Library.DependencyInjection;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Implementations
{
    public class BoostActivator : DestroyService
    {
        private IBoostManager _boostManager;
        [SerializeField] private Boost[] boosts;
        
        public override void Initialize()
        {
            _boostManager = ProjectContext.Instance.GetService<IBoostManager>();
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