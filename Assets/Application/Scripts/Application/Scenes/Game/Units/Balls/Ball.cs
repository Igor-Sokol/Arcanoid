using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallEffectManagers;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallViews;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.MoveController;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.TimeManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls
{
    public class Ball : MonoBehaviour, IReusable
    {
        [SerializeField] private string key;
        [SerializeField] private MoveController moveController;
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private BallHitManager ballHitManager;
        [SerializeField] private BallEffectManager ballEffectManager;
        [SerializeField] private BallView ballView;
        [SerializeField] private CircleCollider2D collisionCollider;
        [SerializeField] private CircleCollider2D triggerCollider;

        public string Key => key;
        public MoveController MoveController => moveController;
        public TimeManager TimeManager => timeManager;
        public BallHitManager BallHitManager => ballHitManager;
        public BallEffectManager BallEffectManager => ballEffectManager;
        public BallView BallView => ballView;
        public bool EnableCollision { get => collisionCollider.enabled; set => collisionCollider.enabled = value; }
        public bool EnableTrigger { get => triggerCollider.enabled; set => triggerCollider.enabled = value; }
        public float Radius => collisionCollider.radius;

        public void PrepareReuse()
        {
            moveController.PrepareReuse();
            TimeManager.ClearTimeScales();
            BallHitManager.PrepareReuse();
            BallHitManager.Initialize(this);
            ballEffectManager.PrepareReuse();
            ballEffectManager.Initialize();
            ballView.PrepareReuse();
            EnableCollision = true;
            EnableTrigger = true;
        }
    }
}