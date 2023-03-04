using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.ObjectPools;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Implementation
{
    public class PoolBlockProvider : BlockProvider, IInitializing
    {
        private Dictionary<string, ObjectPoolMono<Block>> _blockPool;

        [SerializeField] private Transform container;
        [SerializeField] private Block[] prefabs;
        
        public void Initialize()
        {
            _blockPool = new Dictionary<string, ObjectPoolMono<Block>>();

            foreach (var block in prefabs)
            {
                _blockPool.Add(block.Key, new ObjectPoolMono<Block>(() => Instantiate(block), container));
            }
        }
        
        public override Block GetBlock(string key)
        {
            if (_blockPool.TryGetValue(key, out var pool))
            {
                var block = pool.Get();
                block.PrepareReuse();
                return block;
            }

            return null;
        }

        public override void Return(Block block)
        {
            if (_blockPool.TryGetValue(block.Key, out var pool))
            {
                pool.Return(block);
            }
        }
    }
}