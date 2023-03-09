using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.ProcessManagers
{
    public class GameProcessManager : MonoBehaviour
    {
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private HealthManager healthManager;
        [SerializeField] private BallsManager ballsManager;

        private void OnEnable()
        {
            ballsManager.OnAllBallRemoved += OnMissedBalls;
        }
        private void OnDisable()
        {
            ballsManager.OnAllBallRemoved -= OnMissedBalls;
        }
        
        private void OnMissedBalls()
        {
            healthManager.RemoveHealth();

            if (healthManager.CurrentHealth > 0)
            {
                gameplayManager.SetBall();
            }
        }
    }
}