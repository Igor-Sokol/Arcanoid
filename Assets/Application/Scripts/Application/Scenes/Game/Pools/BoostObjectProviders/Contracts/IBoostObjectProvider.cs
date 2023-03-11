using Application.Scripts.Application.Scenes.Game.Units.Boosts.Objects;

namespace Application.Scripts.Application.Scenes.Game.Pools.BoostObjectProviders.Contracts
{
    public interface IBoostObjectProvider
    {
        BoostObject GetBoostObject(string key);
        void Return(BoostObject boostObject);
    }
}