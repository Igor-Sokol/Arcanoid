using System;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Platform.PlatformComponents
{
    public class PlatformSize : MonoBehaviour, IInitializing
    {
        private Vector2 _size;

        [SerializeField] private Vector2 minSizePercentage;
        [SerializeField] private Vector2 maxSizePercentage;
        [SerializeField] private Vector2 startSizePercentage;
        [SerializeField] private Vector2 maxSize;
        
        public Vector2 Size => _size;
        public Vector2 DefaultSize => startSizePercentage;
        public event Action<Vector2> OnSizeChanged;

        public void ChangeSize(Vector2 sizePercentage)
        {
            sizePercentage.x = Mathf.Clamp(sizePercentage.x, minSizePercentage.x, maxSizePercentage.x);
            sizePercentage.y = Mathf.Clamp(sizePercentage.y, minSizePercentage.y, maxSizePercentage.y);

            _size = new Vector2(maxSize.x * sizePercentage.x, maxSize.y * sizePercentage.y);
            
            OnSizeChanged?.Invoke(_size);
        }

        public void Initialize()
        {
            ChangeSize(startSizePercentage);
        }
    }
}
