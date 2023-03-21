using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockViews
{
    public class BlockView : MonoBehaviour, IInitializing, IReusable
    {
        private Sprite _defaultSprite;
        
        [SerializeField] private SpriteRenderer spriteRenderer;

        public Sprite Sprite { get => spriteRenderer.sprite; set => spriteRenderer.sprite = value; }
        
        public void Initialize()
        {
            _defaultSprite = Sprite;
        }
        
        public void PrepareReuse()
        {
            Sprite = _defaultSprite;
        }
    }
}