using System;
using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.PackPlaceAnimators;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.PackPlacers.Contracts;
using Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Screen.UI.PlayerInputs;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers
{
    public class BlockManager : MonoBehaviour, IBlockManager, IReusable
    {
        private Block[][] _blocks;

        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private BlockProvider blockProvider;
        [SerializeField] private PackPlacer packPlacer;
        [SerializeField] private PackPlaceAnimator packPlaceAnimator;

        public IEnumerable<Block> Blocks => _blocks?.SelectMany(blocks => blocks);
        public event Action<Block> OnBlockRemoved;

        public void PrepareReuse()
        {
            if (_blocks != null)
            {
                foreach (var block in Blocks)
                {
                    if (block)
                    {
                        blockProvider.Return(block);
                    }
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
                    var block = blockProvider.GetBlock(blockKeys[i][j]);
                    block.PrepareReuse();
                    _blocks[i][j] = block;
                }
            }
            
            var  positions = packPlacer.Place(_blocks);
            playerInput.enabled = false;
            packPlaceAnimator.Place(_blocks, positions);
            packPlaceAnimator.OnEndAnimation += () => playerInput.enabled = true;
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
                        OnBlockRemoved?.Invoke(block);
                        return;
                    }
                }
            }
        }
    }
}