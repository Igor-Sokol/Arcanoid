using Application.Scripts.Application.Scenes.Game.Units.Balls;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Contracts
{
    public abstract class BallProvider : MonoBehaviour, IBallProvider
    {
        public abstract Ball GetBall(string key);
        public abstract void Return(Ball ball);
    }
}