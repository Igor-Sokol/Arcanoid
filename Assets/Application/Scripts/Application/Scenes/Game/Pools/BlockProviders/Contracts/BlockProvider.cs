using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Contracts
{
    public abstract class BlockProvider : MonoBehaviour, IBlockProvider
    {
        public abstract Block GetBlock(string key);
        public abstract void Return(Block block);
    }
}