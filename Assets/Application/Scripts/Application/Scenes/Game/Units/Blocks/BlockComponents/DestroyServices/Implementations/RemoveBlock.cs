using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Contracts;
using Application.Scripts.Library.DependencyInjection;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Implementations
{
    public class RemoveBlock : DestroyService
    {
        private IBlockManager _blockManager;

        [SerializeField] private Block block;

        [Inject]
        private void Construct(IBlockManager blockManager)
        {
            _blockManager = blockManager;
        }
        
        public override void Initialize()
        {
        }
        
        public override void PrepareReuse()
        {
        }

        public override void OnDestroyAction(Block block)
        {
            _blockManager.RemoveBlock(this.block);
        }
    }
}