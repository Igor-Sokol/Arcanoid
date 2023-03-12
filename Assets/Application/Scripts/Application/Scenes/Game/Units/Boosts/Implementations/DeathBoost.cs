using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Library.DependencyInjection;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations
{
    public class DeathBoost : Boost
    {
        private IHealthManager _healthManager;

        [SerializeField] private int lifeCount;
        
        public override void Initialize()
        {
            _healthManager = ProjectContext.Instance.GetService<IHealthManager>();
        }

        public override float Duration => 0f;
        public override void Enable()
        {
            for (int i = 0; i < lifeCount; i++)
            {
                _healthManager.RemoveHealth();
            }
        }

        public override void Disable()
        {
        }
    }
}