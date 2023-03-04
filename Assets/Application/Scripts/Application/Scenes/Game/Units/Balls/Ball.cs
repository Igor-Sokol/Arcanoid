using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private MoveController moveController;

        public MoveController MoveController => moveController;
    }
}