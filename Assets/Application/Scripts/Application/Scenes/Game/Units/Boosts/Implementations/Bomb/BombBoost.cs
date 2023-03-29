using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.BlocksManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers;
using Application.Scripts.Application.Scenes.Game.Screen.Effects.EnvironmentShakers;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb.BombWayProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb.GameAction;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.TimeManagers;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.Bomb
{
    public class BombBoost : Boost
    {
        private ITimeScaleManager _timeScaleManager;
        private IGameActionManager _gameActionManager;
        private IBlockManager _blockManager;
        private IEnvironmentShake _environmentShake;
        private List<string> _ignoreKeys;
        private ActionHandler _actionHandler;
        private ActionHandler _boostHandler;

        [SerializeField] private BombWay bombWay;
        [SerializeField] private ActionTimeManager actionTimeManager;
        [SerializeField] private ParticleSystem effect;
        [SerializeField] private float blockDestroyDelay;
        [SerializeField] private int damage;
        [SerializeField] private Block[] ignoreBlocks;
        
        public override void Initialize()
        {
            _environmentShake = ProjectContext.Instance.GetService<IEnvironmentShake>();
            _gameActionManager = ProjectContext.Instance.GetService<IGameActionManager>();
            _timeScaleManager = ProjectContext.Instance.GetService<ITimeScaleManager>();
            actionTimeManager.AddTimeScaler(_timeScaleManager.GetTimeScale<GameTimeScale>());
            _blockManager = ProjectContext.Instance.GetService<IBlockManager>();
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
                new BombGameAction(_blockManager, _environmentShake, indexes, blockDestroyDelay, damage, StopBombAction,
                    effect), -1f, actionTimeManager);
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