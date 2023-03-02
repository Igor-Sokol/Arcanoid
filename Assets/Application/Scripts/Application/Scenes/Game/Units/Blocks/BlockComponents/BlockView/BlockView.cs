using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockView
{
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public Vector2 GetWordSize()
        {
            Sprite sprite = spriteRenderer.sprite;
            Vector2 pixels = new Vector2(sprite.rect.width, sprite.rect.height);

            float pixelPerUnit = sprite.pixelsPerUnit;

            return new Vector2(pixels.x / pixelPerUnit, pixels.y / pixelPerUnit);
        }

        public void SetWordSize(Vector2 size)
        {
            Sprite sprite = spriteRenderer.sprite;
            float pixelPerUnit = sprite.pixelsPerUnit;
            Rect imageRect = sprite.rect;

            transform.localScale = new Vector3(imageRect.width / pixelPerUnit / size.x,
                imageRect.height / pixelPerUnit / size.y);
        }
    }
}