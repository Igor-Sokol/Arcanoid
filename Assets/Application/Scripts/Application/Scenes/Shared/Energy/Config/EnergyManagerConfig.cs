using Application.Scripts.Application.Scenes.Shared.Energy.Repository.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.Energy.Config
{
    [CreateAssetMenu(fileName = "EnergyManager", menuName = "Energy/EnergyManagerConfig")]
    public class EnergyManagerConfig : ScriptableObject
    {
        [SerializeField] private int maxGenerateEnergy;
        [SerializeField] private int startEnergy;
        [SerializeField] private float energyGenerateTime;
        [SerializeField] private EnergyRepository energyRepository;

        public int MaxGenerateEnergy => maxGenerateEnergy;
        public int StartEnergy => startEnergy;
        public float EnergyGenerateTime => energyGenerateTime;
        public EnergyRepository EnergyRepository => energyRepository;
    }
}