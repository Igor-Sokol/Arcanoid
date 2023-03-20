using System;
using System.Collections.Generic;
using Application.Scripts.Library.ScreenInfo;
using DG.Tweening;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.ChoosePack.LevelPacks
{
    public class PackViewAnimator : MonoBehaviour
    {
        private Sequence _showAnimation;
        private Sequence _hideAnimation;

        [SerializeField] private ScreenInfo screenInfo;
        [SerializeField] private float packDelay;
        [SerializeField] private float betweenPackDelay;

        public event Action OnAnimationShown;
        public event Action OnAnimationHidden;
        
        public void Show(List<PackView> packs)
        {
            _showAnimation?.Kill();
            _showAnimation = DOTween.Sequence();

            for (int i = 1; i <= packs.Count; i++)
            {
                var pack = packs[i - 1];

                var startPosition = pack.PackContainer.position;
                pack.PackContainer.position = new Vector3(screenInfo.ScreenSize.x / 2 + screenInfo.ScreenRightUpper.x,
                    startPosition.y, startPosition.z);

                _showAnimation.Join(pack.PackContainer
                    .DOMoveX(startPosition.x, packDelay + betweenPackDelay * i));
            }

            _showAnimation.AppendCallback(() => OnAnimationShown?.Invoke());
        }

        public void Hide(PackView pack)
        {
            _hideAnimation?.Kill();
            _hideAnimation = DOTween.Sequence();
            _hideAnimation.Append(pack.PackContainer.DOMoveX(screenInfo.ScreenSize.x / 2 + screenInfo.ScreenRightUpper.x,
                packDelay));
            _hideAnimation.AppendCallback(() => OnAnimationHidden?.Invoke());
        }

        private void OnDisable()
        {
            _showAnimation?.Kill();
            _hideAnimation?.Kill();
        }
    }
}