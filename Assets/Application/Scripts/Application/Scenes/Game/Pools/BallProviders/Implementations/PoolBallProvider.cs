using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Factory;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.ObjectPools;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Implementations
{
    public class PoolBallProvider : BallProvider, IInitializing
    {
        private Dictionary<string, ObjectPoolMono<Ball>> _ballPool;
        private IBallFactory _ballFactory;

        [SerializeField] private Transform container;
        [SerializeField] private Ball[] ballPrefabs;

        [Inject]
        private void Construct(IBallFactory ballFactory)
        {
            _ballFactory = ballFactory;
        }
        
        public void Initialize()
        {
            _ballPool = new Dictionary<string, ObjectPoolMono<Ball>>();

            foreach (var ball in ballPrefabs)
            {
                _ballPool.Add(ball.Key, new ObjectPoolMono<Ball>(() => {
                    var instance = _ballFactory.Create(ball);
                    return instance;
                }, container));
            }
        }
        
        public override Ball GetBall(string key)
        {
            if (_ballPool.TryGetValue(key, out var pool))
            {
                var ball = pool.Get();
                ball.PrepareReuse();
                return ball;
            }

            return null;
        }

        public override void Return(Ball ball)
        {
            if (_ballPool.TryGetValue(ball.Key, out var pool))
            {
                pool.Return(ball);
            }
        }
    }
}