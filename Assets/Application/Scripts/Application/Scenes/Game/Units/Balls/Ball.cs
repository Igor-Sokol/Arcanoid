using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.TimeManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls
{
    public class Ball : MonoBehaviour, IReusable
    {
        [SerializeField] private MoveController moveController;
        [SerializeField] private TimeManager timeManager;

        public MoveController MoveController => moveController;
        public TimeManager TimeManager => timeManager;

        public void PrepareReuse()
        {
            moveController.PrepareReuse();
            TimeManager.ClearTimeScales();
        }
    }
}