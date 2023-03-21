using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts
{
    public interface IBlockManager
    {
        IEnumerable<Block> Blocks { get; }
        Block[][] BlockArray { get; }
        Vector2 GetBlockIndex(Block block);
        event Action<Block> OnBlockRemoved;
        void SetBlocks(BlockInfo[][] blockKeys);
        void RemoveBlock(Block block);
    }
}