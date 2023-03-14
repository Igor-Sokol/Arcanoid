using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Contracts;
using Application.Scripts.Library.EnumerableSafeCollections;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.TimeManagers;
using Sirenix.Utilities;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers
{
    public class BallsManager : MonoBehaviour, IBallManager, IReusable
    {
        private readonly SafeList<Ball> _activeBalls = new SafeList<Ball>();

        [SerializeField] private BallProvider ballProvider;
        [SerializeField] private TimeManager ballTimeManager;
        [SerializeField] private BallHitManager ballHitManager;

        public int BallsCount => _activeBalls.Count;
        public IEnumerable<Ball> Balls => _activeBalls;
        public event Action OnAllBallRemoved;

        public void AddBallService<T>(T service) 
            where T : IBallHitService
        {
            ballHitManager.AddService(service);
            Balls.ForEach(b => b.BallHitManager.AddService(service));
        }

        public void RemoveBallService<T>(T service)
            where T : IBallHitService
        {
            ballHitManager.RemoveService(service);
            Balls.ForEach(b => b.BallHitManager.RemoveService(service));
        }
        
        public Ball GetBall()
        {
            var ball = ballProvider.GetBall();
            _activeBalls.Add(ball);
            ball.PrepareReuse();

            foreach (var service in ballHitManager.Services)
            {
                ball.BallHitManager.AddService(service);
            }

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