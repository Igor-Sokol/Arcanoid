using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts
{
    public interface IBlockManager
    {
        IEnumerable<Block> Blocks { get; }
        Block[][] BlockArray { get; }
        Vector2 GetBlockIndex(Block block);
        event Action<Block> OnBlockRemoved;
        void SetBlocks(string[][] blockKeys);
        void RemoveBlock(Block block);
    }
}