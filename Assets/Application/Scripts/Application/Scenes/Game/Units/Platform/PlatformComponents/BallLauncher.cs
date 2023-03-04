using System;
using Application.Scripts.Application.Scenes.Game.PlayingField.PlayerInputs;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Platform.PlatformComponents
{
    public class BallLauncher : MonoBehaviour
    {
        private Ball _ball;
        
        [SerializeField] private Vector2 defaultLaunchDirection;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlatformMover platformMover;

        public void SetBall(Ball ball)
        {
            if (ball)
            {
                _ball = ball;
                Transform ballTransform = _ball.transform;
                ballTransform.SetParent(transform);
                _ball.MoveController.PhysicActive = false;
                ballTransform.position = transform.position;
                playerInput.OnStopTouching += OnStopTouching;
            }
        }

        public void Launch(Vector2 direction = default)
        {
            if (_ball)
            {
                if (direction == default) direction = defaultLaunchDirection + platformMover.Velocity;

                _ball.MoveController.PhysicActive = true;
                _ball.transform.SetParent(null);
                _ball.MoveController.SetDirection(direction);

                playerInput.OnStopTouching -= OnStopTouching;
            }
        }

        private void OnStopTouching(Vector2 position)
        {
            Launch();
        }
    }
}