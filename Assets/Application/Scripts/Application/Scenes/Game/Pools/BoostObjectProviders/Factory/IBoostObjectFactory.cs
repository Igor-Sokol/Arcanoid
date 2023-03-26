using Application.Scripts.Application.Scenes.Game.Units.Boosts.Objects;

namespace Application.Scripts.Application.Scenes.Game.Pools.BoostObjectProviders.Factory
{
    public interface IBoostObjectFactory
    {
        BoostObject Create(BoostObject prefab);
    }
}