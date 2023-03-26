using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Library.DependencyInjection;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations
{
    public class LifeBoost : Boost
    {
        private IHealthManager _healthManager;

        [SerializeField] private int lifeCount;

        [Inject]
        private void Construct(IHealthManager healthManager)
        {
            _healthManager = healthManager;
        }
        
        public override void Initialize()
        {
        }

        public override float Duration => 0f;
        
        public override void Enable()
        {
            for (int i = 0; i < lifeCount; i++)
            {
                _healthManager.AddHealth();
            }
        }

        public override void Disable()
        {
        }
    }
}