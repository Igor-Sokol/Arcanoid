using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.Energy.Config
{
    [CreateAssetMenu(fileName = "EnergyPrices", menuName = "Energy/EnergyPrices")]
    public class EnergyPriceConfig : ScriptableObject
    {
        [SerializeField] private int levelPrice;
        [SerializeField] private int healthPrice;

        public int LevelPrice => levelPrice;

        public int HealthPrice => healthPrice;
    }
}