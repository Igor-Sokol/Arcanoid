using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Application.Scripts.Library.ScreenInfo;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PlayingField.Borders
{
    public class BallDeadZone : MonoBehaviour
    {
        [SerializeField] private BallsManager ballsManager;
        [SerializeField] private ScreenInfo screenInfo;
        [SerializeField] private ParticleSystem deadParticles;
        [SerializeField] private Vector2 screenPosition;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.TryGetComponent<Ball>(out var ball))
            {
                Instantiate(deadParticles,
                    new Vector3(ball.transform.position.x, screenInfo.PositionFromPercentage(screenPosition).y),
                    Quaternion.identity);
                
                ballsManager.ReturnBall(ball);
            }
        }
    }
}