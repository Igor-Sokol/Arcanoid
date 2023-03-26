using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Factory;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.ObjectPools;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Implementation
{
    public class PoolBlockProvider : BlockProvider, IInitializing
    {
        private Dictionary<string, ObjectPoolMono<Block>> _blockPool;
        private IBlockFactory _blockFactory;

        [SerializeField] private Transform container;
        [SerializeField] private Block[] prefabs;

        [Inject]
        private void Construct(IBlockFactory blockFactory)
        {
            _blockFactory = blockFactory;
        }
        
        public void Initialize()
        {
            _blockPool = new Dictionary<string, ObjectPoolMono<Block>>();

            foreach (var block in prefabs)
            {
                _blockPool.Add(block.Key, new ObjectPoolMono<Block>(() => {
                    var instance = _blockFactory.Create(block);
                    instance.Initialize();
                    return instance;
                }, container));
            }
        }
        
        public override Block GetBlock(string key)
        {
            if (!string.IsNullOrEmpty(key) && _blockPool.TryGetValue(key, out var pool))
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