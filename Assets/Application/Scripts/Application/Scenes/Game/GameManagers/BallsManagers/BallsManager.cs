using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Contracts;
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

        [SerializeField] private BallProvider ballProvider;
        [SerializeField] private TimeManager ballTimeManager;

        public int BallsCount => _activeBalls.Count;
        public IEnumerable<Ball> Balls => _activeBalls;
        public event Action OnAllBallRemoved;
        
        public Ball GetBall()
        {
            var ball = ballProvider.GetBall();
            _activeBalls.Add(ball);
            ball.PrepareReuse();

            foreach (var timeScaler in ballTimeManager.TimeScales)
            {
                ball.TimeManager.AddTimeScaler(timeScaler);
            }
            
            return ball;
        }

        public void ReturnBall(Ball ball)
        {
            _activeBalls.Remove(ball);
            ballProvider.Return(ball);

            if (_activeBalls.Count <= 0)
            {
                OnAllBallRemoved?.Invoke();
            }
        }

        public void PrepareReuse()
        {
            foreach (var ball in _activeBalls)
            {
                ballProvider.Return(ball);
            }
            _activeBalls.Clear();
        }
    }
}