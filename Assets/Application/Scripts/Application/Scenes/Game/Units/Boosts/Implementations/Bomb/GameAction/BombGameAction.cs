using System;
using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Screen.Effects.EnvironmentShakers;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices.Implementation;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb.GameAction
{
    public class BombGameAction : IGameAction
    {
        private readonly IBlockManager _blockManager;
        private readonly IEnvironmentShake _environmentShake;
        private readonly float _blockRemoveDelay;
        private readonly int _damage;
        private readonly Action _onComplete;
        private readonly List<List<Vector2Int>> _indexes;
        private readonly ParticleSystem _particle;
        
        private float _timer;
        
        public BombGameAction(IBlockManager blockManager, IEnvironmentShake environmentShake, IEnumerable<IEnumerable<Vector2Int>> indexes, 
            float blockRemoveDelay, int damage, Action onComplete, ParticleSystem particle)
        {
            _blockManager = blockManager;
            _environmentShake = environmentShake;
            _blockRemoveDelay = blockRemoveDelay;
            _damage = damage;
            _onComplete = onComplete;
            _indexes = new List<List<Vector2Int>>();
            _particle = particle;

            if (indexes != null)
            {
                foreach (var asyncIndexes in indexes)
                {
                    if (asyncIndexes != null)
                    {
                        _indexes.Add(asyncIndexes.ToList());
                    }
                }
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
                    var block = _blockManager.BlockArray[index.y][index.x];
                    
                    if (block)
                    {
                        var health = block.BlockServiceManager.Services.OfType<BlockHealth>().FirstOrDefault();
                        for (int i = 0; i < _damage; i++)
                        {
                            if (health) health.RemoveHealth();
                        }

                        var instance = Object.Instantiate(_particle, block.transform.position, Quaternion.identity);
                        instance.Play();
                        
                        _environmentShake.Shake();
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