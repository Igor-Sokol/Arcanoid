using Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Implementations;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.WeaponBoost.GameActions
{
    public class WeaponPlatformAction : IGameAction
    {
        private readonly TimeScaler _timeScaler;
        private readonly IActiveBallManager _activeBallManager;
        private readonly Platform.Platform _platform;
        private readonly float _shootCooldown;
        private readonly float _ballSpeed;
        private readonly int _damage;
        private readonly string _ballKey;
        
        private float _timer;

        public WeaponPlatformAction(TimeScaler timeScaler, IActiveBallManager activeBallManager, Platform.Platform platform,
            float shootCooldown, float ballSpeed, int damage, string ballKey)
        {
            _timeScaler = timeScaler;
            _activeBallManager = activeBallManager;
            _platform = platform;
            _shootCooldown = shootCooldown;
            _ballSpeed = ballSpeed;
            _damage = damage;
            _ballKey = ballKey;
        }

        public void OnBegin(ActionInfo actionInfo)
        {
        }

        public void OnUpdate(ActionInfo actionInfo)
        {
            _timer -= actionInfo.DeltaTime;

            if (_timer <= 0)
            {
                _timer += _shootCooldown;
                
                
                Vector3 launchPosition =
                    _platform.transform.position + new Vector3(0, _platform.PlatformView.ViewSize.y / 2);

                var rBall = GetBall();
                var lBall = GetBall();
                
                rBall.transform.position = launchPosition + new Vector3(_platform.PlatformView.ViewSize.x / 2, rBall.Radius + 0.1f);
                lBall.transform.position = launchPosition + new Vector3(-_platform.PlatformView.ViewSize.x / 2, lBall.Radius + 0.1f);
                
                rBall.MoveController.SetDirection(Vector2.up);
                lBall.MoveController.SetDirection(Vector2.up);
            }
        }

        public void OnComplete(ActionInfo actionInfo)
        {
        }

        public void OnStop(ActionInfo actionInfo)
        {
        }

        private Ball GetBall()
        {
            var ball = _activeBallManager.GetBall(_ballKey);
            ball.PrepareReuse();

            ball.TimeManagerMono.AddTimeScaler(_timeScaler);
            
            ball.EnableCollision = false;
            ball.BallHitManager.AddService(new BulletBall(OnBulletHit, _damage));
            ball.MoveController.SetSpeed(_ballSpeed);
            ball.MoveController.PhysicActive = true;

            return ball;
        }

        private void OnBulletHit(Ball ball)
        {
            _activeBallManager.Return(ball);
        }
    }
}