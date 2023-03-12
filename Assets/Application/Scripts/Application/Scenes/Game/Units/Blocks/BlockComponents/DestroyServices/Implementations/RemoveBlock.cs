using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Contracts;
using Application.Scripts.Library.DependencyInjection;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Implementations
{
    public class RemoveBlock : DestroyService
    {
        private IBlockManager _blockManager;

        [SerializeField] private Block block;
        
        public override void Initialize()
        {
            _blockManager = ProjectContext.Instance.GetService<IBlockManager>();
        }
        
        public override void PrepareReuse()
        {
        }

        public override void OnDestroyAction()
        {
            _blockManager.RemoveBlock(block);
        }
    }
}