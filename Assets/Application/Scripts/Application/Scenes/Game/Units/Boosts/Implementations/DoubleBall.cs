using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Library.DependencyInjection;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations
{
    public class DoubleBall : Boost
    {
        private IBallManager _ballManager;

        [SerializeField] private Vector2 ballDirection;
        
        public override void Initialize()
        {
            _ballManager = ProjectContext.Instance.GetService<IBallManager>();
        }

        public override float Duration => 0f;
        public override void Enable()
        {
            var currentBalls = _ballManager.Balls;

            foreach (var ball in currentBalls)
            {
                if (ball.MoveController.PhysicActive)
                {
                    var newBall = _ballManager.GetBall();
                    newBall.transform.position = ball.transform.position;
                    newBall.MoveController.PhysicActive = true;
                    newBall.MoveController.SetDirection(ballDirection);
                }
            }
        }

        public override void Disable()
        {
        }
    }
}