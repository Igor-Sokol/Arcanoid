using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Library.DependencyInjection;
using Sirenix.Utilities;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations
{
    public class PlatformSizeBoost : Boost
    {
        private IBoostManager _boostManager;
        private Platform.Platform _platform;

        [SerializeField] private float duration;
        [SerializeField][Range(0f, 1f)] private float platformSizePercentage;
        
        public override void Initialize()
        {
            _boostManager = ProjectContext.Instance.GetService<IBoostManager>();
            _platform = ProjectContext.Instance.GetService<Platform.Platform>();
        }

        public override float Duration => duration;
        public override void Enable()
        {
            _boostManager.GetActiveBoost<PlatformSizeBoost>()?.ForEach(h => h.Stop());
            _platform.PlatformSize.ChangeSize(new Vector2(platformSizePercentage,
                _platform.PlatformSize.DefaultSize.y));
        }

        public override void Disable()
        {
            _platform.PlatformSize.ChangeSize(_platform.PlatformSize.DefaultSize);
        }
    }
}