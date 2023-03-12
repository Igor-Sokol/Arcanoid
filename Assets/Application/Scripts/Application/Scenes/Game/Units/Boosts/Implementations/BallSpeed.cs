using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.TimeManagers.Contracts;
using Sirenix.Utilities;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations
{
    public class BallSpeed : Boost
    {
        private ITimeScaleManager _timeScaleManager;
        private IBoostManager _boostManager;
        private BallTimeScale _ballTimeScale;
        
        [SerializeField] private float duration;
        [SerializeField] private float speedMultiple;
        
        public override void Initialize()
        {
            _timeScaleManager = ProjectContext.Instance.GetService<ITimeScaleManager>();
            _ballTimeScale = _timeScaleManager.GetTimeScale<BallTimeScale>();
            _boostManager = ProjectContext.Instance.GetService<IBoostManager>();
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