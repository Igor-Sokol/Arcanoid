using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.Energy.Repository.Contracts
{
    public abstract class EnergyRepository : MonoBehaviour, IEnergyRepository
    {
        public abstract void Save(EnergySaveObject energySave);
        public abstract EnergySaveObject Load();
    }
}