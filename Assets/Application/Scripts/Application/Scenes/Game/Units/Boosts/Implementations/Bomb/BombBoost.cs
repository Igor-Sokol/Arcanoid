using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb.BombWayProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb.GameAction;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.TimeManagers;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb
{
    public class BombBoost : Boost
    {
        private ITimeScaleManager _timeScaleManager;
        private IGameActionManager _gameActionManager;
        private IBlockManager _blockManager;
        private List<string> _ignoreKeys;
        private ActionHandler _actionHandler;
        private ActionHandler _boostHandler;

        [SerializeField] private BombWay bombWay;
        [SerializeField] private ActionTimeManager actionTimeManager;
        [SerializeField] private ParticleSystem effect;
        [SerializeField] private float blockDestroyDelay;
        [SerializeField] private int damage;
        [SerializeField] private Block[] ignoreBlocks;
        
        [Inject]
        private void Construct(IGameActionManager gameActionManager, ITimeScaleManager timeScaleManager, IBlockManager blockManager)
        {
            _gameActionManager = gameActionManager;
            _timeScaleManager = timeScaleManager;
            _blockManager = blockManager;
        }
        
        public override void Initialize()
        {
            actionTimeManager.AddTimeScaler(_timeScaleManager.GetTimeScale<GameTimeScale>());
            _ignoreKeys = new List<string>(ignoreBlocks.Select(b => b.Key));
        }

        public override void RegisterHandler(ActionHandler handler)
        {
            _boostHandler = handler;
        }

        public override float Duration => -1f;
        public override void Enable()
        {
            var indexes = bombWay.GetIndexes(_blockManager.BlockArray, _blockManager.GetBlockIndex(Block), _ignoreKeys);

            _actionHandler = _gameActionManager.StartAction(
                new BombGameAction(_blockManager, indexes, blockDestroyDelay, damage, StopBombAction, effect), -1f, actionTimeManager);
        }

        public override void Disable()
        {
            _actionHandler.Stop();
        }

        private void OnDestroy()
        {
            Disable();
        }

        private void StopBombAction()
        {
            _actionHandler.Stop();
            _boostHandler.Stop();
        }
    }
}