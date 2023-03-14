using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.MoveController;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.TimeManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls
{
    public class Ball : MonoBehaviour, IReusable
    {
        [SerializeField] private MoveController moveController;
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private BallHitManager ballHitManager;

        public MoveController MoveController => moveController;
        public TimeManager TimeManager => timeManager;
        public BallHitManager BallHitManager => ballHitManager;

        public void PrepareReuse()
        {
            moveController.PrepareReuse();
            TimeManager.ClearTimeScales();
            BallHitManager.PrepareReuse();
        }
    }
}