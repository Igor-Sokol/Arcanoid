using System;
using Application.Scripts.Application.Scenes.Shared.UI.EnergyViews;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.LosePopUp.Animators
{
    public class LosePopUpAnimator : PopUpAnimator
    {
        private Sequence _activeAnimation;
        private int _startEnergy;
        private int _maxEnergy;

        [SerializeField] private RectTransform popUp;
        [SerializeField] private Image popUpImage;
        [SerializeField] private Vector2 popUpPositionOffset;
        [SerializeField] private TMP_Text title;
        [SerializeField] private EnergyView energyView;
        [SerializeField] private Button[] buttons;
        [SerializeField] private float popUpDuration;
        [SerializeField] private float textDuration;
        [SerializeField] private float energyShowDuration;
        [SerializeField] private float buttonDelay;
        [SerializeField] private float hideDelay;
        
        public override event Action OnAnimationShown;
        public override event Action OnAnimationHidden;
        
        public void Configure(int startEnergy, int maxEnergy)
        {
            _startEnergy = startEnergy;
            _maxEnergy = maxEnergy;
        }
        
        [Button("PlayShowAnimation")]
        private void PlayShowAnimation()
        {
            ShowAnimation();
        }
        
        [Button("PlayHideAnimation")]
        private void PlayHideAnimation()
        {
            HideAnimation();
        }
        
        public override void ShowAnimation()
        {
            var pixelRect = (popUpImage.canvas.transform as RectTransform)?.rect ?? popUpImage.canvas.pixelRect;
            var rect = popUp.rect;
            var lossyScale = popUp.lossyScale.x;
            float rightOffscreenPosition = (rect.width + pixelRect.width) / 2 * lossyScale;
            float leftOffscreenPosition = -rightOffscreenPosition;
            float downOffscreenPosition = -(rect.height + pixelRect.height) / 2 * lossyScale;
            Vector2 center = Vector2.zero;
            
            _activeAnimation?.Kill();
            _activeAnimation = DOTween.Sequence();

            popUp.anchoredPosition = Vector2.zero;
            _activeAnimation.Append(popUpImage.transform
                .DOMoveY(center.y + popUpPositionOffset.y * lossyScale, popUpDuration)
                .From(downOffscreenPosition));

            string titleText = title.text;
            title.text = string.Empty;
            _activeAnimation.Append(title.DOText(titleText, textDuration));

            energyView.SetProgressImmediately(0, _maxEnergy);
            energyView.SetTimeLeft(0);
            _activeAnimation.Append(energyView.transform
                .DOMoveX(center.x, energyShowDuration)
                .From(rightOffscreenPosition));
            _activeAnimation.Append(energyView.SetProgress(_startEnergy, _maxEnergy));
            _activeAnimation.AppendInterval(0);

            for (int i = 1; i <= buttons.Length; i++)
            {
                var button = buttons[i - 1];
                
                _activeAnimation.Join(button.transform
                    .DOMoveX(center.x, popUpDuration + buttonDelay * i)
                    .From(leftOffscreenPosition));
            }
            
            _activeAnimation.AppendCallback(() => OnAnimationShown?.Invoke());
        }

        public override void HideAnimation()
        {
            var pixelRect = (popUpImage.canvas.transform as RectTransform)?.rect ?? popUpImage.canvas.pixelRect;
            var rect = popUp.rect;
            var lossyScale = popUp.lossyScale.x;
            float rightOffscreenPosition = (rect.width + pixelRect.width) / 2 * lossyScale;

            _activeAnimation?.Kill();
            _activeAnimation = DOTween.Sequence();

            _activeAnimation.AppendInterval(hideDelay);
            _activeAnimation.Append(popUpImage.transform
                .DOMoveX(rightOffscreenPosition, popUpDuration));

            for (int i = 1; i <= buttons.Length; i++)
            {
                var button = buttons[i - 1];
                
                _activeAnimation.Join(button.transform
                    .DOMoveX(rightOffscreenPosition, popUpDuration + buttonDelay * i));
            }
            
            _activeAnimation.AppendCallback(() => OnAnimationHidden?.Invoke());
            _activeAnimation.SetEase(Ease.InQuad);
        }
    }
}