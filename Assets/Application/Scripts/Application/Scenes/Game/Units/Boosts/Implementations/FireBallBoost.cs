using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallEffectManagers.Implementations;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Implementations;
using Application.Scripts.Library.DependencyInjection;
using Sirenix.Utilities;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations
{
    public class FireBallBoost : Boost
    {
        private IBoostManager _boostManager;
        private IBallManager _ballManager;
        private FireBall _fireBallService;
        
        [SerializeField] private float duration;
        [SerializeField] private FireEffect fireEffect;
        
        public override float Duration => duration;
        
        public override void Initialize()
        {
            _ballManager = ProjectContext.Instance.GetService<IBallManager>();
            _boostManager = ProjectContext.Instance.GetService<IBoostManager>();
        }
        
        public override void Enable()
        {
            var boosts = _boostManager.GetActiveBoost<FireBallBoost>();
            if (boosts != null)
            {
                boosts.ForEach(h => h.ChangeTime(duration));
            }
            else
            {
                if (_fireBallService != null) Disable();
            
                _fireBallService = new FireBall();
                _ballManager.AddBallService(_fireBallService);
                _ballManager.AddBallEffect(fireEffect);
            }
        }

        public override void Disable()
        {
            _ballManager.RemoveBallService(_fireBallService);
            _ballManager.RemoveBallEffect(fireEffect);
            _fireBallService = null;
        }
    }
}