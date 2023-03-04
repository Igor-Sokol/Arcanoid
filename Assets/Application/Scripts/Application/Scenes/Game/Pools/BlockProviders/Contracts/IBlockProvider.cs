using Application.Scripts.Application.Scenes.Game.Units.Blocks;

namespace Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Contracts
{
    public interface IBlockProvider
    {
        Block GetBlock(string key);
        void Return(Block block);
    }
}