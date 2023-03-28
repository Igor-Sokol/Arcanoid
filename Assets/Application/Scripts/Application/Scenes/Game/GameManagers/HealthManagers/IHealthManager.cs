using System;
using Application.Scripts.Library.Reusable;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers
{
    public interface IHealthManager : IReusable
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }

        event Action OnHealthAdded;
        event Action OnHealthRemoved;
        event Action OnDead;
        event Action OnPrepareReuse;
        void AddHealth();
        void RemoveHealth();
    }
}