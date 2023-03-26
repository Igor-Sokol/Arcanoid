using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallEffectManagers.Implementations;
using Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.FireBoost.FireBallSetUpActions;
using Application.Scripts.Library.DependencyInjection;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.FireBoost
{
    public class FireBallBoost : Boost
    {
        private IBoostManager _boostManager;
        private IBallManager _ballManager;

        [SerializeField] private float duration;
        [SerializeField] private FireEffect fireEffect;
        [SerializeField] private Color color;
        
        public override float Duration => duration;

        [Inject]
        private void Construct(IBallManager ballManager, IBoostManager boostManager)
        {
            _ballManager = ballManager;
            _boostManager = boostManager;
        }
        
        public override void Initialize()
        {
        }
        
        public override void Enable()
        {
            var boosts = _boostManager.GetActiveBoost<FireBallBoost>();
            var actionHandlers = boosts?.ToList();
            if (actionHandlers?.Any(a => a.Valid) ?? false)
            {
                actionHandlers.ForEach(h => h.ChangeTime(duration));
            }
            else
            {
                var action = new FireBallSetUpAction(fireEffect, color);
                _ballManager.BallSetUpManager.AddAction(action);
            }
        }

        public override void Disable()
        {
            _ballManager.BallSetUpManager.RemoveAction(typeof(FireBallSetUpAction));
        }
    }
}