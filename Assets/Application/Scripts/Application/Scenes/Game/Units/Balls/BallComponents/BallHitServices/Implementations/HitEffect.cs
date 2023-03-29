using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Implementations
{
    public class HitEffect : BallHitService
    {
        [SerializeField] private ParticleSystem particle;
        
        public override void PrepareReuse()
        {
        }

        public override void OnCollisionAction(Collision2D col, Ball ball)
        {
            Instantiate(particle, ball.transform.position, Quaternion.identity);
        }

        public override void OnTriggerAction(Collider2D col, Ball ball)
        {
        }
    }
}