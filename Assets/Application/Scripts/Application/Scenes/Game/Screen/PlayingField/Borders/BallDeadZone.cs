using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PlayingField.Borders
{
    public class BallDeadZone : MonoBehaviour
    {
        [SerializeField] private BallsManager ballsManager;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.TryGetComponent<Ball>(out var ball))
            {
                ballsManager.ReturnBall(ball);
            }
        }
    }
}