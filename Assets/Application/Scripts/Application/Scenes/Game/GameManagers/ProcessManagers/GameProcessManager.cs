using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Library.Reusable;
using UnityEngine;
using UnityEngine.Serialization;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.ProcessManagers
{
    public class GameProcessManager : MonoBehaviour, IReusable
    {
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private BallManager ballManager;

        public bool IgnoreBall { get; set; }
        
        public void PrepareReuse()
        {
            IgnoreBall = false;
        }
        
        private void OnEnable()
        {
            ballManager.OnAllBallRemoved += OnMissedBall;
        }
        private void OnDisable()
        {
            ballManager.OnAllBallRemoved -= OnMissedBall;
        }
        
        private void OnMissedBall()
        {
            if (IgnoreBall) return;
            
            gameplayManager.SetBall();
        }
    }
}