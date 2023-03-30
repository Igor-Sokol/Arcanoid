using System;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Shared.UI.PopUps.MessagePopUp
{
    public class MessagePopUpAnimator : PopUpAnimator
    {
        private Sequence _activeAnimation;
        
        [SerializeField] private RectTransform popUp;
        [SerializeField] private Image popUpImage;
        [SerializeField] private Button[] buttons;
        [SerializeField] private float popUpDuration;
        [SerializeField] private float buttonDelay;
        
        public override event Action OnAnimationShown;
        public override event Action OnAnimationHidden;
        
        public override void ShowAnimation()
        {
            var pixelRect = (popUpImage.canvas.transform as RectTransform)?.rect ?? popUpImage.canvas.pixelRect;
            var rect = popUp.rect;
            var lossyScale = popUp.lossyScale.x;
            float startPosition = (rect.width + pixelRect.width) / 2 * lossyScale;
            float center = 0f;

            _activeAnimation?.Kill();
            _activeAnimation = DOTween.Sequence();
            
            _activeAnimation.Append(popUp.transform
                .DOMoveX(center, popUpDuration)
                .From(startPosition));

            for (int i = 1; i <= buttons.Length; i++)
            {
                var button = buttons[i - 1];
                
                _activeAnimation.Join(button.transform
                    .DOMoveX(center, popUpDuration + buttonDelay * i)
                    .From(startPosition));
            }

            _activeAnimation.AppendCallback(() => OnAnimationShown?.Invoke());
        }

        public override void HideAnimation()
        {
            var pixelRect = (popUpImage.canvas.transform as RectTransform)?.rect ?? popUpImage.canvas.pixelRect;
            var rect = popUp.rect;
            var lossyScale = popUp.lossyScale.x;
            float targetPosition = (rect.width + pixelRect.width) / 2 * lossyScale;
            
            _activeAnimation?.Kill();
            _activeAnimation = DOTween.Sequence();
            
            _activeAnimation.Append(popUp.transform
                .DOMoveX(targetPosition, popUpDuration));

            for (int i = 1; i <= buttons.Length; i++)
            {
                var button = buttons[i - 1];
                
                _activeAnimation.Join(button.transform
                    .DOMoveX(targetPosition, popUpDuration + buttonDelay * i));
            }
            
            _activeAnimation.AppendCallback(() => OnAnimationHidden?.Invoke());
            _activeAnimation.SetEase(Ease.InQuad);
        }
    }
}