using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts
{
    public interface IBlockManager
    {
        IEnumerable<Block> Blocks { get; }
        event Action<Block> OnBlockRemoved;
        void SetBlocks(string[][] blockKeys);
        void RemoveBlock(Block block);
    }
}