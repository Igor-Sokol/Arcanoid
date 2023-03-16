using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Implementations;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.WeaponBoost.GameActions
{
    public class WeaponPlatformAction : IGameAction
    {
        private readonly IBallManager _ballManager;
        private readonly IBallProvider _ballProvider;
        private readonly Platform.Platform _platform;
        private readonly float _shootCooldown;
        private readonly float _ballSpeed;
        private readonly int _damage;

        private float _timer;

        public WeaponPlatformAction(IBallManager ballManager, IBallProvider ballProvider, Platform.Platform platform,
            float shootCooldown, float ballSpeed, int damage)
        {
            _ballManager = ballManager;
            _ballProvider = ballProvider;
            _platform = platform;
            _shootCooldown = shootCooldown;
            _ballSpeed = ballSpeed;
            _damage = damage;
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
            var ball = _ballProvider.GetBall();
            ball.PrepareReuse();

            foreach (var timeScaler in _ballManager.BallTimeManager.TimeScales)
            {
                ball.TimeManager.AddTimeScaler(timeScaler);
            }
            
            ball.EnableCollision = false;
            ball.BallHitManager.AddService(new BulletBall(_ballProvider, _damage));
            ball.MoveController.SetSpeed(_ballSpeed);
            ball.MoveController.PhysicActive = true;

            return ball;
        }
    }
}