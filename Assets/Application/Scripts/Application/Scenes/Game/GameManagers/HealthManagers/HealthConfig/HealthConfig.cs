using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers.HealthConfig
{
    [CreateAssetMenu(fileName = "Health", menuName = "Health/Config")]
    public class HealthConfig : ScriptableObject, IHealthConfig
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private int startHealth;

        public int MaxHealth => maxHealth;
        public int StartHealth => startHealth;
    }
}