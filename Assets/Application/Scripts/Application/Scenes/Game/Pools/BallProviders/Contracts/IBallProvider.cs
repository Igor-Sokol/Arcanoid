using Application.Scripts.Application.Scenes.Game.Units.Balls;

namespace Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Contracts
{
    public interface IBallProvider
    {
        Ball GetBall(string key);
        void Return(Ball ball);
    }
}