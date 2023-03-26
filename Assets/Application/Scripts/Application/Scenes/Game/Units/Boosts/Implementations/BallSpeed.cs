using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.TimeManagers.Contracts;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations
{
    public class BallSpeed : Boost
    {
        private ITimeScaleManager _timeScaleManager;
        private IBoostManager _boostManager;
        private BallTimeScale _ballTimeScale;
        
        [SerializeField] private float duration;
        [SerializeField] private float speedMultiple;

        [Inject]
        private void Construct(ITimeScaleManager timeScaleManager, IBoostManager boostManager)
        {
            _timeScaleManager = timeScaleManager;
            _boostManager = boostManager;
        }
        
        public override void Initialize()
        {
            _ballTimeScale = _timeScaleManager.GetTimeScale<BallTimeScale>();
        }

        public override float Duration => duration;
        public override void Enable()
        {
            _boostManager.GetActiveBoost<BallSpeed>()?.ForEach(h => h.Stop());
            _ballTimeScale.Scale *= speedMultiple;
        }

        public override void Disable()
        {
            _ballTimeScale.Scale /= speedMultiple;
        }
    }
}