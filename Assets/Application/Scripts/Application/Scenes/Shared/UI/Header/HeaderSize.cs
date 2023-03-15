using System.Linq;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.UI.Header
{
    public class HeaderSize : MonoBehaviour
    {
        [SerializeField] private RectTransform[] transforms;

        public Vector2 Size => new Vector2(transforms.Max(t => t.rect.width * t.lossyScale.x),
            transforms.Max(t => t.rect.height * t.lossyScale.y));
    }
}