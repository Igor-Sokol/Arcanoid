using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Contracts;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.ServiceCollections;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices
{
    public class BallHitManager : ServiceCollection<IBallHitService>, IReusable
    {
        private Ball _ball;

        public void Initialize(Ball ball)
        {
            _ball = ball;
        }
        
        public void PrepareReuse()
        {
            Clear();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            foreach (var service in Services)
            {
                service.OnCollisionAction(col, _ball);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            foreach (var service in Services)
            {
                service.OnTriggerAction(col, _ball);
            }
        }
    }
}