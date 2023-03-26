using System;
using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Configs;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.GameActions;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.PackPlaceAnimators;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.PackPlacers.Contracts;
using Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Screen.UI.PlayerInputs;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.Configs;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.Contracts;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.TimeManagers;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Reusable;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers
{
    public class BlockManager : MonoBehaviour, IBlockManager, IReusable, IInitializing
    {
        private readonly Dictionary<string, BlockConfig> _blockConfigs = new Dictionary<string, BlockConfig>();

        private IGameActionManager _gameActionManager;
        private ActionHandler _destroyAllAction;
        private Block[][] _blocks;

        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private BlockProvider blockProvider;
        [SerializeField] private PackPlacer packPlacer;
        [SerializeField] private PackPlaceAnimator packPlaceAnimator;
        [SerializeField] private ActionTimeManager actionTimeManager;
        [SerializeField] private BlockScriptableConfig[] blockConfigs;
        [SerializeField] private float forceDestroyTime;

        public IEnumerable<Block> Blocks => _blocks?.SelectMany(blocks => blocks);
        public Block[][] BlockArray => _blocks;
        public event Action<Block> OnBlockRemoved;

        [Inject]
        private void Construct(IGameActionManager gameActionManager)
        {
            _gameActionManager = gameActionManager;
        }
        
        public void Initialize()
        {
            foreach (var config in blockConfigs)
            {
                _blockConfigs.Add(config.ConfigKey, config.Config);
            }
        }
        
        public Vector2Int GetBlockIndex(Block block)
        {
            for (int i = 0; i < _blocks.Length; i++)
            {
                for (int j = 0; j < _blocks[i].Length; j++)
                {
                    if (_blocks[i][j] == block)
                    {
                        return new Vector2Int(j, i);
                    }
                }
            }

            return new Vector2Int(-1, -1);
        }
        public void PrepareReuse()
        {
            _destroyAllAction.Stop();
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
        public void SetBlocks(BlockInfo[][] blockKeys)
        {
            _blocks = new Block[blockKeys.Length][];

            for (int i = 0; i < blockKeys.Length; i++)
            {
                _blocks[i] = new Block[blockKeys[i].Length];
                for (int j = 0; j < blockKeys[i].Length; j++)
                {
                    var block = blockProvider.GetBlock(blockKeys[i][j].BlockKey);
                    if (block) block.PrepareReuse();

                    if (blockKeys[i][j].ConfigKey != null)
                    {
                        block.Configure(_blockConfigs[blockKeys[i][j].ConfigKey]);
                    }
                    
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
        public void DestroyAllBlocks()
        {
            _destroyAllAction.Stop();
            _destroyAllAction = _gameActionManager.StartAction(new DestroyAllBlocks(this, forceDestroyTime / _blocks.Length),
                -1, actionTimeManager);
        }
        private void OnDisable()
        {
            _destroyAllAction.Stop();
        }
    }
}