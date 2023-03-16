using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers;
using Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.WeaponBoost.GameActions;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.TimeManagers;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.TimeManagers.Contracts;
using Sirenix.Utilities;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.WeaponBoost
{
    public class WeaponPlatform : Boost
    {
        private IGameActionManager _gameActionManager;
        private IBoostManager _boostManager;
        private ITimeScaleManager _timeScaleManager;
        private GameTimeScale _gameTimeScale;
        private IBallProvider _ballProvider;
        private Platform.Platform _platform;
        private ActionHandler _actionHandler;

        [SerializeField] private ActionTimeManager actionTimeManager;
        [SerializeField] private float duration;
        [SerializeField] private float shootCooldown;
        [SerializeField] private float ballSpeed;
        [SerializeField] private int damage;

        public override float Duration => duration;
        
        public override void Initialize()
        {
            _gameActionManager = ProjectContext.Instance.GetService<IGameActionManager>();
            _boostManager = ProjectContext.Instance.GetService<IBoostManager>();
            _ballProvider = ProjectContext.Instance.GetService<IBallProvider>();
            _platform = ProjectContext.Instance.GetService<Platform.Platform>();
            _timeScaleManager = ProjectContext.Instance.GetService<ITimeScaleManager>();
            _gameTimeScale = _timeScaleManager.GetTimeScale<GameTimeScale>();
            actionTimeManager.AddTimeScaler(_gameTimeScale);
        }
        
        public override void Enable()
        {
            _boostManager.GetActiveBoost<WeaponPlatform>()?.ForEach(h => h.Stop());
            
            _actionHandler.Stop();
            _actionHandler = _gameActionManager.StartAction(
                new WeaponPlatformAction(_gameTimeScale, _ballProvider, _platform, shootCooldown, ballSpeed, damage), -1f,
                actionTimeManager);
        }

        public override void Disable()
        {
            _actionHandler.Stop();
        }

        private void OnDestroy()
        {
            _actionHandler.Stop();
        }
    }
}