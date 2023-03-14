using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Implementations
{
    public class FireBall : IBallHitService
    {
        public Ball Ball { get; set; }
        
        public void PrepareReuse()
        {
        }

        public void OnCollisionAction(Collision2D col)
        {
            if (col.collider.TryGetComponent<Block>(out var block))
            {
                block.DestroyServiceManager.Destroy();
                Ball.MoveController.SetDirection(Ball.MoveController.PreviousDirection);
            }
        }

        public void OnTriggerAction(Collider2D col)
        {
            if (col.TryGetComponent<Block>(out var block))
            {
                block.DestroyServiceManager.Destroy();
            }
        }
    }
}