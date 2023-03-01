using Application.Scripts.Application.Scenes.Game.Units.Blocks;

namespace Application.Scripts.Application.Scenes.Game.Pools.BlockProvider.Contracts
{
    public interface IBlockProvider
    {
        Block GetBlock(string key);
    }
}