using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using UnityEngine;
using UnityEngine.Serialization;

namespace Application.Scripts.Application.Scenes.Game.Screen.PlayingField.Borders
{
    public class BallDeadZone : MonoBehaviour
    {
        [FormerlySerializedAs("ballsManager")] [SerializeField] private BallManager ballManager;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.TryGetComponent<Ball>(out var ball))
            {
                ballManager.ReturnBall(ball);
            }
        }
    }
}