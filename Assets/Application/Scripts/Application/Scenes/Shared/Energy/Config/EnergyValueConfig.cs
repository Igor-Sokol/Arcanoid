using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.Energy.Config
{
    [CreateAssetMenu(fileName = "Energy", menuName = "Energy/EnergyValues")]
    public class EnergyValueConfig : ScriptableObject
    {
        [SerializeField] private int levelPrice;
        [SerializeField] private int healthPrice;
        [SerializeField] private int winGift;

        public int LevelPrice => levelPrice;
        public int HealthPrice => healthPrice;
        public int WinGift => winGift;
    }
}