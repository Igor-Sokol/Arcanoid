using System.Collections.Generic;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.SceneManagers.Contracts.Loading;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.SceneManagers.Loading
{
    public class DefaultSceneLoading : SceneLoading, IInitializing
    {
        private readonly List<Image> _images = new List<Image>();

        private Sequence _animation;

        [SerializeField] private Canvas canvas;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private Image prefab;
        [SerializeField] private Sprite[] blocks;
        [SerializeField] private float animationTime;
        [SerializeField] private Vector2 defaultImageSize;
        
        public void Initialize()
        {
            float scale = (Screen.width / (float)gridLayoutGroup.constraintCount) / defaultImageSize.x;

            Vector2 imageSize = defaultImageSize * scale;
            gridLayoutGroup.cellSize = defaultImageSize * scale;
            
            var imageCount = new Vector2Int(gridLayoutGroup.constraintCount, Mathf.CeilToInt(Screen.height / imageSize.y));

            int count = imageCount.x * imageCount.y;
            for (int i = 0; i < count; i++)
            {
                var instance = Instantiate(prefab, gridLayoutGroup.transform);
                instance.color = Color.clear;
                instance.sprite = blocks[i % blocks.Length];
                _images.Add(instance);
            }
        }
        
        public override YieldInstruction Enable()
        {
            canvas.gameObject.SetActive(true);
            Show();
            return new WaitForSeconds(animationTime);
        }

        public override YieldInstruction Disable()
        {
            Hide();
            return new WaitForSeconds(animationTime);
        }

        private void Show()
        {
            gridLayoutGroup.enabled = true;
            for (int i = 0; i < _images.Count; i++)
            {
                int index = Random.Range(i, _images.Count);
                (_images[i], _images[index]) = (_images[index], _images[i]);
                _images[i].transform.SetSiblingIndex(i);
            }
            
            _animation?.Kill();
            _animation = DOTween.Sequence();

            float itemDuration = animationTime / _images.Count;

            foreach (var image in _images)
            {
                var color = Color.white;
                color.a = 0f;
                image.color = color;
                
                _animation.Append(image.DOFade(1, itemDuration));
            }
        }

        private void Hide()
        {
            gridLayoutGroup.enabled = false;
            
            _animation?.Kill();
            _animation = DOTween.Sequence();

            float itemDuration = animationTime / _images.Count;
            
            foreach (var image in _images)
            {
                var color = Color.white;
                color.a = 1f;
                image.color = color;
                
                _animation.Append(image.DOFade(0, itemDuration));
            }
            
            _animation.OnComplete(() => canvas.gameObject.SetActive(false));
        }
    }
}