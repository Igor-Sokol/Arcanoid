using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers;
using Application.Scripts.Library.DependencyInjection;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations
{
    public class LifeBoost : Boost
    {
        private IHealthManager _healthManager;
        
        public override void Initialize()
        {
            _healthManager = ProjectContext.Instance.GetService<IHealthManager>();
        }

        public override float Duration => 0f;
        
        public override void Enable()
        {
            _healthManager.AddHealth();
        }

        public override void Disable()
        {
        }
    }
}