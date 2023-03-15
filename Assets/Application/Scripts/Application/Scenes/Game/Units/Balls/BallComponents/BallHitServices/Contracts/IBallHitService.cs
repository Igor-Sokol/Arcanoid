using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Contracts
{
    public interface IBallHitService : IReusable
    {
        void OnCollisionAction(Collision2D col, Ball ball);
        void OnTriggerAction(Collider2D col, Ball ball);
    }
}