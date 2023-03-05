using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.PackPlacers.Contracts;
using Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers
{
    public class BlockManager : MonoBehaviour, IReusable
    {
        private Block[][] _blocks;
        
        [SerializeField] private BlockProvider blockProvider;
        [SerializeField] private PackPlacer packPlacer;

        public IEnumerable<Block> Blocks => _blocks?.SelectMany(blocks => blocks);

        public void PrepareReuse()
        {
            if (_blocks != null)
            {
                foreach (var block in Blocks)
                {
                    block.PrepareReuse();
                    blockProvider.Return(block);
                }
                
                _blocks = null;
            }
        }
        
        public void SetBlocks(string[][] blockKeys)
        {
            _blocks = new Block[blockKeys.Length][];

            for (int i = 0; i < blockKeys.Length; i++)
            {
                _blocks[i] = new Block[blockKeys[i].Length];
                for (int j = 0; j < blockKeys[i].Length; j++)
                {
                    _blocks[i][j] = blockProvider.GetBlock(blockKeys[i][j]);
                }
            }
            
            packPlacer.Place(_blocks);
        }

        public void RemoveBlock(Block block)
        {
            foreach (var blocks in _blocks)
            {
                for (int i = 0; i < blocks.Length; i++)
                {
                    if (blocks[i] == block)
                    {
                        blocks[i] = null;
                        blockProvider.Return(block);
                        return;
                    }
                }
            }
        }
    }
}