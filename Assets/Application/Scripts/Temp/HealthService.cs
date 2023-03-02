using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Contracts;
using UnityEngine;

namespace Application.Scripts.Temp
{
    public class HealthService : HitService
    {
        private float _actualHealth;
        
        [SerializeField] private float health;
        
        public override void OnHitAction()
        {
            _actualHealth--;
        }

        public override void PrepareReuse()
        {
            _actualHealth = health;
        }
    }
}