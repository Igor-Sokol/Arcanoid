using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.TimeManagers.Contracts;
using Sirenix.Utilities;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations
{
    public class PlatformSpeed : Boost
    {
        private ITimeScaleManager _timeScaleManager;
        private IBoostManager _boostManager;
        private PlatformTimeScale _platformTimeScale;
        
        [SerializeField] private float duration;
        [SerializeField] private float speedMultiple;
        
        public override void Initialize()
        {
            _timeScaleManager = ProjectContext.Instance.GetService<ITimeScaleManager>();
            _platformTimeScale = _timeScaleManager.GetTimeScale<PlatformTimeScale>();
            _boostManager = ProjectContext.Instance.GetService<IBoostManager>();
        }

        public override float Duration => duration;
        public override void Enable()
        {
            _boostManager.GetActiveBoost<PlatformSpeed>()?.ForEach(h => h.Stop());
            _platformTimeScale.Scale *= speedMultiple;
        }

        public override void Disable()
        {
            _platformTimeScale.Scale /= speedMultiple;
        }
    }
}