using System;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers;
using Application.Scripts.Application.Scenes.Game.Screen.UI.LevelPackViews;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Implementations;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.ProcessManagers
{
    public class BlockProgressManager : MonoBehaviour, IReusable
    {
        private int _brokenBlocks;
        private int _totalBlocks;
        
        [SerializeField] private BlockManager blockManager;
        [SerializeField] private LevelPackView levelPackView;

        public int BrokenBlocks => _brokenBlocks;
        public int TotalBlocks => _totalBlocks;
        public event Action OnBlockBroken;
        public event Action OnAllBlockBroken;

        private void OnEnable()
        {
            blockManager.OnBlockRemoved += UpdateProgress;
        }
        private void OnDisable()
        {
            blockManager.OnBlockRemoved -= UpdateProgress;
        }

        public void PrepareReuse()
        {
            _totalBlocks =
                blockManager.Blocks.Count(b =>
                {
                    if (!b) return false;
                    return b.HitServiceManager.Services.OfType<BlockHealthService>().Any();
                });
            _brokenBlocks = 0;
            
            levelPackView.LevelProgress.SetProgress(_brokenBlocks / _totalBlocks * 100, 100);
        }

        private void UpdateProgress(Block block)
        {
            if (block.HitServiceManager.Services.OfType<BlockHealthService>().Any())
            {
                _brokenBlocks++;
                levelPackView.LevelProgress.SetProgress((int)(_brokenBlocks / (float)_totalBlocks * 100f), 100);

                OnBlockBroken?.Invoke();
                
                if (_brokenBlocks == _totalBlocks)
                {
                    OnAllBlockBroken?.Invoke();
                }
            }
        }
    }
}