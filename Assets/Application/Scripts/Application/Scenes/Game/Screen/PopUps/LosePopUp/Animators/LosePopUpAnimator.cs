using System;
using Application.Scripts.Application.Scenes.Shared.UI.EnergyViews;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using DG.Tweening;
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
        
        [SerializeField] private Image popUp;
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
        
        public override void ShowAnimation()
        {
            var pixelRect = popUp.canvas.pixelRect;
            var rect = popUp.rectTransform.rect;
            var lossyScale = popUp.rectTransform.lossyScale.x;
            float rightOffscreenPosition = rect.width * lossyScale / 2 + pixelRect.width;
            float leftOffscreenPosition = 0 - rect.width * lossyScale / 2;
            float downOffscreenPosition = 0 - rect.height * lossyScale / 2;
            Vector2 center = new Vector2(pixelRect.width / 2, pixelRect.height / 2);
            
            _activeAnimation?.Kill();
            _activeAnimation = DOTween.Sequence();

            _activeAnimation.Append(popUp.transform
                .DOMove(center + popUpPositionOffset, popUpDuration)
                .From(new Vector3(center.x, downOffscreenPosition, 0)));

            string titleText = title.text;
            title.text = string.Empty;
            _activeAnimation.Append(title.DOText(titleText, textDuration));

            energyView.SetProgressImmediately(0, _maxEnergy);
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
            float rightOffscreenPosition = popUp.rectTransform.rect.width * popUp.rectTransform.lossyScale.x / 2 +
                                           popUp.canvas.pixelRect.width;
            
            _activeAnimation?.Kill();
            _activeAnimation = DOTween.Sequence();

            _activeAnimation.AppendInterval(hideDelay);
            _activeAnimation.Append(popUp.transform
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

        public void Configure(int startEnergy, int maxEnergy)
        {
            _startEnergy = startEnergy;
            _maxEnergy = maxEnergy;
        }
    }
}