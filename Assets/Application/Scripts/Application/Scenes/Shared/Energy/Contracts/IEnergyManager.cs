using System;

namespace Application.Scripts.Application.Scenes.Shared.Energy.Contracts
{
    public interface IEnergyManager
    {
        int CurrentEnergy { get; }
        int MaxEnergy { get; }
        event Action OnEnergyAdded;
        event Action OnEnergyRemoved;
        event Action<float> OnFillTimeChanged;
        void AddEnergy(int energy);
        void RemoveEnergy(int energy);
    }
}