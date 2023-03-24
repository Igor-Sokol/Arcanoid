using Application.Scripts.Application.Scenes.Shared.DoTweenGameActions;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.TimeManagers;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.InitializeManager.Contracts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.UI.Background
{
    public class LineAnimator : MonoBehaviour, IInitializing
    {
        private IGameActionManager _gameActionManager;
        private Sequence _animation;
        private ActionHandler _animationHandler;
        private int _colorIndex;
    
        [SerializeField] private Image image;
        [SerializeField] private ActionTimeManager actionTimeManager;
        [SerializeField] private Color[] colors;
        [SerializeField] private float stepDuration;
        [SerializeField] private float fadeTime;
        
        public void Initialize()
        {
            _gameActionManager = ProjectContext.Instance.GetService<IGameActionManager>();
        }
        
        private void Play()
        {
            _animationHandler.Stop();
            
            _animation?.Kill();
            _animation = DOTween.Sequence();

            image.fillAmount = 0;
            _animation.Append(image.DOFillAmount(1, stepDuration).SetEase(Ease.Linear));
            _animation.Append(image.DOFade(0, fadeTime).SetEase(Ease.Linear));
            _animation.AppendCallback(() =>
            {
                if (_colorIndex >= colors.Length)
                {
                    _colorIndex = 0;
                }
                
                image.fillAmount = 0;
                image.color = colors[_colorIndex];

                _colorIndex++;
            });
            _animation.SetLoops(-1);
            
            _animationHandler = _gameActionManager.StartAction(new DoTweenGameAction(_animation), -1, actionTimeManager);
        }

        private void Stop()
        {
            _animationHandler.Stop();
            _animation?.Kill();
        }

        private void OnEnable()
        {
            Initialize();
            Play();
        }

        private void OnDisable() => Stop();
    }
}
