using System;
using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices.Implementation;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb.GameAction
{
    public class BombGameAction : IGameAction
    {
        private readonly IBlockManager _blockManager;
        private readonly float _blockRemoveDelay;
        private readonly Action _onComplete;
        private readonly List<List<Vector2>> _indexes;
        
        private float _timer;
        
        public BombGameAction(IBlockManager blockManager, IEnumerable<IEnumerable<Vector2>> indexes, 
            float blockRemoveDelay, Action onComplete)
        {
            _blockManager = blockManager;
            _blockRemoveDelay = blockRemoveDelay;
            _onComplete = onComplete;
            _indexes = new List<List<Vector2>>();

            foreach (var asyncIndexes in indexes)
            {
                _indexes.Add(asyncIndexes.ToList());
            }
        }
        
        public void OnBegin(ActionInfo actionInfo)
        {
        }

        public void OnUpdate(ActionInfo actionInfo)
        {
            _timer += actionInfo.DeltaTime;

            if (_timer >= _blockRemoveDelay)
            {
                _timer = 0f;
                RemoveBlock();
            }
        }

        public void OnComplete(ActionInfo actionInfo)
        {
        }

        public void OnStop(ActionInfo actionInfo)
        {
        }

        private void RemoveBlock()
        {
            foreach (var asyncIndexes in _indexes)
            {
                if (asyncIndexes.Count > 0)
                {
                    var index = asyncIndexes[0];
                    var block = _blockManager.BlockArray[(int)index.y][(int)index.x];
                    
                    if (block)
                    {
                        block.BlockServiceManager.Services.OfType<BlockHealth>().FirstOrDefault()?.RemoveHealth();
                    }

                    asyncIndexes.RemoveAt(0);
                }
            }

            _indexes.RemoveAll(a => a.Count <= 0);

            if (_indexes.Count <= 0)
            {
                _onComplete?.Invoke();
            }
        }
    }
}