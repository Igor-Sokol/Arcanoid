using System.Collections.Generic;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts
{
    public interface IBoostManager
    {
        void Execute<T>(IEnumerable<T> boosts) where T : IBoost;
        void Execute<T>(T boost) where T : IBoost;
    }
}