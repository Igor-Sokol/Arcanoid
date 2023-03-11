using System;
using System.Collections.Generic;
using Application.Scripts.Library.ScreenInfo;
using DG.Tweening;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.ChoosePack.LevelPacks
{
    public class PackViewAnimator : MonoBehaviour
    {
        private Sequence _activeAnimation;

        [SerializeField] private ScreenInfo screenInfo;
        [SerializeField] private float packDelay;
        [SerializeField] private float betweenPackDelay;

        public event Action OnAnimationShown;
        public event Action OnAnimationHidden;
        
        public void Show(List<PackView> packs)
        {
            _activeAnimation?.Kill();
            _activeAnimation = DOTween.Sequence();

            for (int i = 1; i <= packs.Count; i++)
            {
                var pack = packs[i - 1];

                var startPosition = pack.PackContainer.position;
                pack.PackContainer.position = new Vector3(screenInfo.ScreenSize.x / 2 + screenInfo.ScreenRightUpper.y,
                    startPosition.y, startPosition.z);

                _activeAnimation.Join(pack.PackContainer
                    .DOMoveX(startPosition.x, packDelay + betweenPackDelay * i));
            }

            _activeAnimation.AppendCallback(() => OnAnimationShown?.Invoke());
        }

        public void Hide(PackView pack)
        {
            _activeAnimation?.Kill();
            _activeAnimation = DOTween.Sequence();
            _activeAnimation.Append(pack.PackContainer.DOMoveX(screenInfo.ScreenSize.x / 2 + screenInfo.ScreenRightUpper.y,
                packDelay));
            _activeAnimation.AppendCallback(() => OnAnimationHidden?.Invoke());
        }

        private void OnDisable()
        {
            _activeAnimation?.Kill();
        }
    }
}