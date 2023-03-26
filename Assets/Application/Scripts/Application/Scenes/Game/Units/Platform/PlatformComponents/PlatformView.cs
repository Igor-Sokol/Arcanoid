using Application.Scripts.Application.Scenes.Shared.DoTweenGameActions;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.TimeManagers;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.Units.Platform.PlatformComponents
{
    public class PlatformView : MonoBehaviour
    {
        private IGameActionManager _gameActionManager;
        private ActionHandler _sizeAnimationHandler;
        
        [SerializeField] private BoxCollider2D boxCollider2d;
        [SerializeField] private SpriteRenderer wingSpriteRenderer;
        [SerializeField] private PlatformSize platformSize;
        [SerializeField] private ActionTimeManager actionTimeManager;
        [SerializeField] private float changeSizeTime;

        public Vector2 ViewSize => wingSpriteRenderer.size;

        [Inject]
        private void Construct(IGameActionManager gameActionManager)
        {
            _gameActionManager = gameActionManager;
        }

        private void OnEnable()
        {
            platformSize.OnSizeChanged += OnPlatformSizeChanged;
        }

        private void OnDisable()
        {
            platformSize.OnSizeChanged -= OnPlatformSizeChanged;
            StopAnimation();
        }

        private void OnPlatformSizeChanged(Vector2 size)
        {
            StopAnimation();
            
            var sizeAnimation = DOTween.Sequence();
            
            sizeAnimation.Append(DOTween.To(() => boxCollider2d.size, (newSize) => boxCollider2d.size = newSize,
                new Vector2(size.x, boxCollider2d.size.y), changeSizeTime));
            sizeAnimation.Join(DOTween.To(() => wingSpriteRenderer.size, (newSize) => wingSpriteRenderer.size = newSize,
                new Vector2(size.x, wingSpriteRenderer.size.y), changeSizeTime));
            sizeAnimation.OnComplete(StopAnimation);

            _sizeAnimationHandler =
                _gameActionManager.StartAction(new DoTweenGameAction(sizeAnimation), -1f, actionTimeManager);
        }
        
        private void StopAnimation() => _sizeAnimationHandler.Stop();
    }
}
