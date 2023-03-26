using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.WeaponBoost.GameActions;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.TimeManagers;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.WeaponBoost
{
    public class WeaponPlatform : Boost
    {
        private IGameActionManager _gameActionManager;
        private IBoostManager _boostManager;
        private ITimeScaleManager _timeScaleManager;
        private GameTimeScale _gameTimeScale;
        private IActiveBallManager _activeBallManager;
        private Platform.Platform _platform;
        private ActionHandler _actionHandler;

        [SerializeField] private ActionTimeManager actionTimeManager;
        [SerializeField] private float duration;
        [SerializeField] private float shootCooldown;
        [SerializeField] private float ballSpeed;
        [SerializeField] private int damage;
        [SerializeField] private string bulletBallKey;

        public override float Duration => duration;

        [Inject]
        private void Construct(IGameActionManager gameActionManager, IBoostManager boostManager, ITimeScaleManager timeScaleManager,
            Platform.Platform platform, IActiveBallManager activeBallManager)
        {
            _gameActionManager = gameActionManager;
            _boostManager = boostManager;
            _timeScaleManager = timeScaleManager;
            _platform = platform;
            _activeBallManager = activeBallManager;
        }
        
        public override void Initialize()
        {
            _gameTimeScale = _timeScaleManager.GetTimeScale<GameTimeScale>();
            actionTimeManager.AddTimeScaler(_gameTimeScale);
        }
        
        public override void Enable()
        {
            var boosts = _boostManager.GetActiveBoost<WeaponPlatform>();
            var actionHandlers = boosts?.ToList();
            if (actionHandlers?.Any(a => a.Valid) ?? false)
            {
                actionHandlers.ForEach(h => h.ChangeTime(duration));
            }
            else
            {
                _actionHandler.Stop();
                _actionHandler = _gameActionManager.StartAction(
                    new WeaponPlatformAction(_gameTimeScale, _activeBallManager, _platform, shootCooldown, ballSpeed, damage, bulletBallKey), -1f,
                    actionTimeManager);
            }
        }

        public override void Disable()
        {
            _actionHandler.Stop();
        }
    }
}