using Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.ObjectPools;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Implementations
{
    public class PoolBallProvider : BallProvider, IInitializing
    {
        private ObjectPoolMono<Ball> _ballPool;

        [SerializeField] private Ball ballPrefab;
        
        public void Initialize()
        {
            _ballPool = new ObjectPoolMono<Ball>(() => Object.Instantiate(ballPrefab), transform);
        }

        public override Ball GetBall()
        {
            return _ballPool.Get();
        }

        public override void Return(Ball ball)
        {
            _ballPool.Return(ball);
        }
    }
}