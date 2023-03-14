using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Implementations;
using Application.Scripts.Library.DependencyInjection;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations
{
    public class FireBallBoost : Boost
    {
        private IBallManager _ballManager;
        private FireBall _fireBallService;
        
        [SerializeField] private float duration;
        
        public override float Duration => duration;
        
        public override void Initialize()
        {
            _ballManager = ProjectContext.Instance.GetService<IBallManager>();
        }
        
        public override void Enable()
        {
            if (_fireBallService != null) Disable();
            
            _fireBallService = new FireBall();
            _ballManager.AddBallService(_fireBallService);
        }

        public override void Disable()
        {
            _ballManager.RemoveBallService(_fireBallService);
            _fireBallService = null;
        }
    }
}