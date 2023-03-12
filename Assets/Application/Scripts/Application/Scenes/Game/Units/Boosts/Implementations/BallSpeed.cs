using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations
{
    public class BallSpeed : Boost
    {
        private ITimeScaleManager _timeScaleManager;
        private BallTimeScale _ballTimeScale;
        
        [SerializeField] private float duration;
        [SerializeField] private float speedMultiple;
        
        public override void Initialize()
        {
            _timeScaleManager = ProjectContext.Instance.GetService<ITimeScaleManager>();
            _ballTimeScale = _timeScaleManager.GetTimeScale<BallTimeScale>();
        }

        public override float Duration => duration;
        public override void Enable()
        {
            _ballTimeScale.Scale *= speedMultiple;
        }

        public override void Disable()
        {
            _ballTimeScale.Scale /= speedMultiple;
        }
    }
}