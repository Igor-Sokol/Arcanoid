using Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Application.Scripts.Library.EnumerableSafeCollections;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers
{
    public class ActiveBallManager : MonoBehaviour, IActiveBallManager, IReusable
    {
        private readonly SafeList<Ball> _bulletBalls = new SafeList<Ball>();

        [SerializeField] private BallProvider ballProvider;
        
        public Ball GetBall(string key)
        {
            var bulletBall = ballProvider.GetBall(key);
            _bulletBalls.Add(bulletBall);
            return bulletBall;
        }

        public void Return(Ball ball)
        {
            ball.PrepareReuse();
            _bulletBalls.Remove(ball);
            ballProvider.Return(ball);
        }

        public void PrepareReuse()
        {
            foreach (var bulletBall in _bulletBalls)
            {
                ballProvider.Return(bulletBall);
            }
            _bulletBalls.Clear();
        }
    }
}