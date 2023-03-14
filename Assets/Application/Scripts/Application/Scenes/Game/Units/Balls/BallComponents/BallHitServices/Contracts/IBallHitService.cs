using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Contracts
{
    public interface IBallHitService : IReusable
    {
        Ball Ball { get; set; }
        void OnCollisionAction(Collision2D col);
        void OnTriggerAction(Collider2D col);
    }
}