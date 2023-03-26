using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Library.DependencyInjection;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations
{
    public class PlatformSizeBoost : Boost
    {
        private IBoostManager _boostManager;
        private Platform.Platform _platform;

        [SerializeField] private float duration;
        [SerializeField][Range(0f, 1f)] private float platformSizePercentage;

        [Inject]
        private void Construct(IBoostManager boostManager, Platform.Platform platform)
        {
            _boostManager = boostManager;
            _platform = platform;
        }
        
        public override void Initialize()
        {
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