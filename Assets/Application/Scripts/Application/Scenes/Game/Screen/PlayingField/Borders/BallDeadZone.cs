using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Screen.PlayingField.Borders
{
    public class BallDeadZone : MonoBehaviour
    {
        private IBallManager _ballManager;

        [Inject]
        private void Construct(IBallManager ballManager)
        {
            _ballManager = ballManager;
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.TryGetComponent<Ball>(out var ball))
            {
                _ballManager.ReturnBall(ball);
            }
        }
    }
}