using System;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;
using Application.Scripts.Application.Scenes.Shared.UI.EnergyViews;
using Application.Scripts.Application.Scenes.Shared.UI.StringViews;
using Application.Scripts.Library.Localization.LocalizationManagers;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps.Animators
{
    public class WinPopUpAnimator : PopUpAnimator
    {
        private Sequence _activeAnimation;
        private int _startEnergy;
        private int _maxEnergy;
        private int _addedEnergy;
        private Sprite _packImage;
        private string _packName;
        private int _packProgress;
        private int _packLevelCount;

        [SerializeField] private RectTransform popUp;
        [SerializeField] private Image popUpImage;
        [SerializeField] private Vector2 popUpPositionOffset;
        [SerializeField] private TMP_Text title;
        [SerializeField] private EnergyView energyView;
        [SerializeField] private RectTransform packBackground;
        [SerializeField] private Image packImage;
        [SerializeField] private Image fadePackImage;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private TMP_Text packName;
        [SerializeField] private ProgressView packProgress;
        [SerializeField] private Button[] buttons;
        [SerializeField] private float popUpDuration;
        [SerializeField] private float titleDuration;
        [SerializeField] private float textDuration;
        [SerializeField] private float energyShowDuration;
        [SerializeField] private float packBackgroundDuration;
        [SerializeField] private float changePackImageDuration;
        [SerializeField] private float buttonDelay;
        [SerializeField] private float hideDelay;
        
        public override event Action OnAnimationShown;
        public override event Action OnAnimationHidden;
        
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
            
            _activeAnimation.Append(title.DOScale(1, titleDuration).From(0).SetEase(Ease.OutBounce));

            packImage.sprite = _packImage;
            _activeAnimation.Append(packBackground.transform
                .DOMoveX(center.x, packBackgroundDuration)
                .From(leftOffscreenPosition));
            _activeAnimation.AppendCallback(() => particle.Play());
            
            packName.text = string.Empty;
            _activeAnimation.Append(packName.DOText(_packName, textDuration));

            packProgress.SetProgressImmediately(_packProgress - 1, _packLevelCount);
            _activeAnimation.Append(packProgress.SetProgress(_packProgress, _packLevelCount));
            
            energyView.SetProgressImmediately(_startEnergy, _maxEnergy);
            energyView.SetTimeLeft(0);
            _activeAnimation.Append(energyView.transform
                .DOMoveX(center.x, energyShowDuration)
                .From(rightOffscreenPosition));
            _activeAnimation.Append(energyView.SetProgress(_startEnergy + _addedEnergy, _maxEnergy));
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

            if (packName.text != _packName)
            {
                fadePackImage.sprite = _packImage;
                _activeAnimation.Append(packImage.DOFade(0, changePackImageDuration));
                _activeAnimation.Join(packName.DOText(_packName, textDuration));
                _activeAnimation.Join(packProgress.SetProgress(0, _packLevelCount));
                _activeAnimation.AppendCallback(() =>
                {
                    packImage.sprite = _packImage;
                    packImage.color = Color.white;
                });
            }
            
            _activeAnimation.Append(popUpImage.transform
                .DOMoveX(rightOffscreenPosition, popUpDuration));

            for (int i = 1; i <= buttons.Length; i++)
            {
                var button = buttons[i - 1];
                
                _activeAnimation.Join(button.transform
                    .DOMoveX(rightOffscreenPosition, popUpDuration + buttonDelay * i));
            }

            _activeAnimation.AppendCallback(() =>
                particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear));
            _activeAnimation.AppendCallback(() => OnAnimationHidden?.Invoke());
            _activeAnimation.SetEase(Ease.InQuad);
        }
        
        public void Configure(int startEnergy, int maxEnergy, int addedEnergy)
        {
            _startEnergy = startEnergy;
            _maxEnergy = maxEnergy;
            _addedEnergy = addedEnergy;
        }
        
        public void Configure(IPackInfo packInfo, ILocalizationManager localizationManager)
        {
            _packImage = packInfo.LevelPack.PackImage;
            _packName = localizationManager.GetString(packInfo.LevelPack.PackNameKey) ?? string.Empty;
            _packProgress = packInfo.CurrentLevelIndex + 1;
            _packLevelCount = packInfo.LevelPack.LevelCount;
        }
    }
}