using Application.Scripts.Application.Scenes.Game.Units.Balls;

namespace Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Factory
{
    public interface IBallFactory
    {
        Ball Create(Ball prefab);
    }
}