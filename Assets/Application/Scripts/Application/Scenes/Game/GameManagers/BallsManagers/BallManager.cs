using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.BallSetUpServices.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Application.Scripts.Library.EnumerableSafeCollections;
using Application.Scripts.Library.TimeManagers;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers
{
    public class BallManager : MonoBehaviour, IBallManager
    {
        private readonly SafeList<Ball> _activeBalls = new SafeList<Ball>();
        private IActiveBallManager _activeBallManager;
        private IBallSetUpManager _ballSetUpManager;

        [SerializeField] private Ball ballKey;
        [SerializeField] private TimeManager ballTimeManager;

        public int BallsCount => _activeBalls.Count;
        public IEnumerable<Ball> Balls => _activeBalls;
        public IBallSetUpManager BallSetUpManager => _ballSetUpManager;
        public event Action OnAllBallRemoved;

        [Inject]
        private void Construct(IActiveBallManager activeBallManager, IBallSetUpManager ballSetUpManager)
        {
            _activeBallManager = activeBallManager;
            _ballSetUpManager = ballSetUpManager;
        }
        
        public Ball GetBall()
        {
            var ball = _activeBallManager.GetBall(ballKey.Key);
            _activeBalls.Add(ball);
            ball.PrepareReuse();

            foreach (var action in _ballSetUpManager.BallSetUpActions)
            {
                action.Install(ball);
            }
            
            foreach (var timeScaler in ballTimeManager.TimeScales)
            {
                ball.TimeManager.AddTimeScaler(timeScaler);
            }
            
            return ball;
        }

        public void ReturnBall(Ball ball)
        {
            if (_activeBalls.Count > 0)
            {
                _activeBalls.Remove(ball);
                _activeBallManager.Return(ball);

                if (_activeBalls.Count <= 0)
                {
                    OnAllBallRemoved?.Invoke();
                }
            }
        }

        public void PrepareReuse()
        {
            foreach (var ball in _activeBalls)
            {
                _activeBallManager.Return(ball);
            }
            _activeBalls.Clear();
            _ballSetUpManager.PrepareReuse();
        }

        private void OnEnable()
        {
            BallSetUpManager.OnActionAdded += AddBallService;
            BallSetUpManager.OnActionRemoved += RemoveBallService;
        }

        private void OnDisable()
        {
            BallSetUpManager.OnActionAdded -= AddBallService;
            BallSetUpManager.OnActionRemoved -= RemoveBallService;
        }

        private void AddBallService(IBallSetUpAction action)
        {
            foreach (var ball in Balls)
            {
                action.Install(ball);
            }
        }
        
        private void RemoveBallService(IBallSetUpAction action)
        {
            foreach (var ball in Balls)
            {
                action.UnInstall(ball);
            }
        }
    }
}