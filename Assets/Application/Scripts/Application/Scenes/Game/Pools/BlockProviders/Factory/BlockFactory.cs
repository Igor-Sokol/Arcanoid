using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Factory
{
    public class BlockFactory : IBlockFactory
    {
        private readonly DiContainer _diContainer;
        
        public BlockFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public Block Create(Block prefab)
        {
            return _diContainer.InstantiatePrefabForComponent<Block>(prefab);
        }
    }
}