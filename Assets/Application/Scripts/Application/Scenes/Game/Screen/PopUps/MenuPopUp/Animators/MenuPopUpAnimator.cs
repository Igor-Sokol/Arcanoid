using System;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.MenuPopUp.Animators
{
    public class MenuPopUpAnimator : PopUpAnimator
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
            var pixelRect = popUpImage.canvas.pixelRect;
            float startPosition = popUp.rect.width * popUp.lossyScale.x / 2 +
                                  pixelRect.width;
            float center = pixelRect.width / 2;

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
            float targetPosition = popUp.rect.width * popUp.lossyScale.x / 2 +
                                   popUpImage.canvas.pixelRect.width;
            
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