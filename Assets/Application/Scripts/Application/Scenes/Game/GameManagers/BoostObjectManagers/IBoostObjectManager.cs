using Application.Scripts.Application.Scenes.Game.Units.Boosts.Objects;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BoostObjectManagers
{
    public interface IBoostObjectManager
    {
        BoostObject GetBoostView(string key);
        void ReturnBoostView(BoostObject boostObject);
    }
}