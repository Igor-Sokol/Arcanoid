using Application.Scripts.Library.InitializeManager.Contracts;
using DG.Tweening;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Platform.PlatformComponents
{
    public class PlatformView : MonoBehaviour, IInitializing
    {
        private Sequence _sizeAnimation;
        
        [SerializeField] private BoxCollider2D boxCollider2d;
        [SerializeField] private SpriteRenderer wingSpriteRenderer;
        [SerializeField] private PlatformSize platformSize;
        [SerializeField] private float changeSizeTime;

        public void Initialize()
        {
            OnPlatformSizeChanged(platformSize.Size);
        }
        
        private void OnEnable()
        {
            platformSize.OnSizeChanged += OnPlatformSizeChanged;
        }

        private void OnDisable()
        {
            platformSize.OnSizeChanged -= OnPlatformSizeChanged;
        }

        private void OnPlatformSizeChanged(Vector2 size)
        {
            _sizeAnimation?.Kill();
            _sizeAnimation = DOTween.Sequence();

            _sizeAnimation.Append(DOTween.To(() => boxCollider2d.size, (newSize) => boxCollider2d.size = newSize,
                new Vector2(size.x, boxCollider2d.size.y), changeSizeTime));
            _sizeAnimation.Join(DOTween.To(() => wingSpriteRenderer.size, (newSize) => wingSpriteRenderer.size = newSize,
                new Vector2(size.x, wingSpriteRenderer.size.y), changeSizeTime));
        }
    }
}
