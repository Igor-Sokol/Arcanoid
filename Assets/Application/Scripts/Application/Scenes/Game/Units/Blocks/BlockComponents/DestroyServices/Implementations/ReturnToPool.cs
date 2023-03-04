using Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Contracts;
using Application.Scripts.Library.DependencyInjection;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Implementations
{
    public class ReturnToPool : DestroyService
    {
        private IBlockProvider _blockProvider;

        [SerializeField] private Block block;
        
        public override void PrepareReuse()
        {
            _blockProvider = ProjectContext.Instance.GetService<IBlockProvider>();
        }

        public override void OnDestroyAction()
        {
            _blockProvider.Return(block);
        }
    }
}