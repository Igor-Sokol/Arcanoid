using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Platform.PlatformComponents
{
    public class PlatformView : MonoBehaviour, IInitializing
    {
        [SerializeField] private BoxCollider2D boxCollider2d;
        [SerializeField] private SpriteRenderer wingSpriteRenderer;
        [SerializeField] private PlatformSize platformSize;

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
            boxCollider2d.size = new Vector2(size.x, boxCollider2d.size.y);
            wingSpriteRenderer.size = new Vector2(size.x, wingSpriteRenderer.size.y);
        }
    }
}
