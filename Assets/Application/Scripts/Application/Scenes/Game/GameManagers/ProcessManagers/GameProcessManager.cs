using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.GameplayManagers;
using Application.Scripts.Library.Reusable;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.ProcessManagers
{
    public class GameProcessManager : MonoBehaviour, IReusable
    {
        private IBallManager _ballManager;
        
        [SerializeField] private GameplayManager gameplayManager;

        public bool IgnoreBall { get; set; }

        [Inject]
        private void Construct(IBallManager ballManager)
        {
            _ballManager = ballManager;
        }
        
        public void PrepareReuse()
        {
            IgnoreBall = false;
        }
        
        private void OnEnable()
        {
            _ballManager.OnAllBallRemoved += OnMissedBall;
        }
        private void OnDisable()
        {
            _ballManager.OnAllBallRemoved -= OnMissedBall;
        }
        
        private void OnMissedBall()
        {
            if (IgnoreBall) return;
            
            gameplayManager.SetBall();
        }
    }
}