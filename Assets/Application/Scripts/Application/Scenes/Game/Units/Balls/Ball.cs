using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls
{
    public class Ball : MonoBehaviour, IReusable
    {
        [SerializeField] private MoveController moveController;

        public MoveController MoveController => moveController;
        public void PrepareReuse()
        {
            moveController.PrepareReuse();
        }
    }
}