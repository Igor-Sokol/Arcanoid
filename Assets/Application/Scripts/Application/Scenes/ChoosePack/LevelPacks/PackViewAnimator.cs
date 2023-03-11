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
        
        public void Show(List<PackView> packs)
        {
            _activeAnimation?.Kill();
            _activeAnimation = DOTween.Sequence();

            for (int i = 1; i <= packs.Count; i++)
            {
                var pack = packs[i - 1];

                var startPosition = pack.PackContainer.position;
                pack.PackContainer.position = new Vector3(screenInfo.ScreenSize.x + screenInfo.ScreenRightUpper.y,
                    startPosition.y, startPosition.z);

                _activeAnimation.Join(pack.PackContainer
                    .DOMoveX(startPosition.x, packDelay * i));
            }
        }

        private void OnDisable()
        {
            _activeAnimation?.Kill();
        }
    }
}