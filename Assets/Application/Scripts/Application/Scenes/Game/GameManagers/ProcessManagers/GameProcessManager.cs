using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.ProcessManagers
{
    public class GameProcessManager : MonoBehaviour, IReusable
    {
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private BallsManager ballsManager;

        public bool IgnoreBall { get; set; }
        
        public void PrepareReuse()
        {
            IgnoreBall = false;
        }
        
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
            if (IgnoreBall) return;
            
            gameplayManager.SetBall();
        }
    }
}