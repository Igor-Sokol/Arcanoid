using Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers;
using Application.Scripts.Application.Scenes.Shared.DoTweenGameActions;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.TimeManagers;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.TimeManagers.Contracts;
using DG.Tweening;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Objects
{
    public class BoostObjectAnimator : MonoBehaviour, IInitializing, IReusable
    {
        private ITimeScaleManager _timeScaleManager;
        private IGameActionManager _gameActionManager;
        private ActionHandler _animation;

        [SerializeField] private Transform boostView;
        [SerializeField] private float speed;
        [SerializeField] private ActionTimeManager actionTimeManager;
        
        public void Initialize()
        {
            _timeScaleManager = ProjectContext.Instance.GetService<ITimeScaleManager>();
            actionTimeManager.AddTimeScaler(_timeScaleManager.GetTimeScale<GameTimeScale>());
            
            _gameActionManager = ProjectContext.Instance.GetService<IGameActionManager>();
        }

        public void PrepareReuse()
        {
            _animation.Stop();
            
            var tween = boostView.DOMoveY(-1f, 1 / speed)
                .SetRelative()
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
            
            _animation = _gameActionManager.StartAction(new DoTweenGameAction(tween), -1, actionTimeManager);
        }
        
        private void OnDisable()
        {
            _animation.Stop();
        }
    }
}