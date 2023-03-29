using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.BallSetUpServices;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.BallSetUpServices.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Application.Scripts.Library.EnumerableSafeCollections;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.TimeManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers
{
    public class BallsManager : MonoBehaviour, IBallManager, IReusable
    {
        private readonly SafeList<Ball> _activeBalls = new SafeList<Ball>();

        [SerializeField] private Ball ballKey;
        [SerializeField] private ActiveBallManager activeBallManager;
        [SerializeField] private TimeManager ballTimeManager;
        [SerializeField] private BallSetUpManager ballSetUpManager;

        public int BallsCount => _activeBalls.Count;
        public IEnumerable<Ball> Balls => _activeBalls;
        public BallSetUpManager BallSetUpManager => ballSetUpManager;
        public event Action OnAllBallRemoved;

        public Ball GetBall()
        {
            var ball = activeBallManager.GetBall(ballKey.Key);
            _activeBalls.Add(ball);
            ball.PrepareReuse();

            foreach (var action in ballSetUpManager.BallSetUpActions)
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
            ball.PrepareReuse();
            
            if (_activeBalls.Count > 0)
            {
                _activeBalls.Remove(ball);
                activeBallManager.Return(ball);

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
                activeBallManager.Return(ball);
            }
            _activeBalls.Clear();
            ballSetUpManager.PrepareReuse();
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