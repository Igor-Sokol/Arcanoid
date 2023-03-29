using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Contracts
{
    public abstract class BallHitService : MonoBehaviour, IBallHitService
    {
        public abstract void PrepareReuse();
        public abstract void OnCollisionAction(Collision2D col, Ball ball);
        public abstract void OnTriggerAction(Collider2D col, Ball ball);
    }
}