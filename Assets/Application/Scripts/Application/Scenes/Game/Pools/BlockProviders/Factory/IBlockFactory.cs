using Application.Scripts.Application.Scenes.Game.Units.Blocks;

namespace Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Factory
{
    public interface IBlockFactory
    {
        Block Create(Block prefab);
    }
}