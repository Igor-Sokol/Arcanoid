using System;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;
using Application.Scripts.Application.Scenes.Shared.UI.EnergyViews;
using Application.Scripts.Application.Scenes.Shared.UI.StringViews;
using Application.Scripts.Library.Localization.LocalizationManagers;
using Application.Scripts.Library.PopUpManagers.AnimationContracts;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps.Animators.WinAnimator
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
        private Sprite _nextPackImage;
        private string _nextPackName;
        private int _nextPackLevelCount;

        [SerializeField] private RectTransform popUp;
        [SerializeField] private Image popUpImage;
        [SerializeField] private Vector2 popUpPositionOffset;
        [SerializeField] private TMP_Text title;
        [SerializeField] private EnergyView energyView;
        [SerializeField] private RectTransform packBackground;
        [SerializeField] private Image packImage;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private TMP_Text packName;
        [SerializeField] private ProgressView packProgress;
        [SerializeField] private Transform packInfoContainer;
        [SerializeField] private Button[] buttons;
        [SerializeField] private WinPopUpAnimatorScriptableConfig animatorConfig;

        public override event Action OnAnimationShown;
        public override event Action OnAnimationHidden;

        public void Configure(int startEnergy, int maxEnergy, int addedEnergy)
        {
            _startEnergy = startEnergy;
            _maxEnergy = maxEnergy;
            _addedEnergy = addedEnergy;
        }
        public void Configure(IPackInfo currentPackInfo, IPackInfo nextPackInfo, ILocalizationManager localizationManager)
        {
            _packImage = currentPackInfo.LevelPack.PackImage;
            _packName = localizationManager.GetString(currentPackInfo.LevelPack.PackNameKey) ?? string.Empty;
            _packProgress = currentPackInfo.CurrentLevelIndex + 1;
            _packLevelCount = currentPackInfo.LevelPack.LevelCount;
            _nextPackImage = nextPackInfo.LevelPack.PackImage;
            _nextPackName = localizationManager.GetString(nextPackInfo.LevelPack.PackNameKey) ?? string.Empty;
            _nextPackLevelCount = nextPackInfo.LevelPack.LevelCount;
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
                .DOMoveY(center.y + popUpPositionOffset.y * lossyScale, animatorConfig.AnimatorConfig.PopUpDuration)
                .From(downOffscreenPosition));
            
            _activeAnimation.Append(title.DOScale(1, animatorConfig.AnimatorConfig.TitleDuration).From(0).SetEase(Ease.OutBounce));

            packImage.sprite = _packImage;
            _activeAnimation.Append(packBackground.transform
                .DOMoveX(center.x, animatorConfig.AnimatorConfig.PackBackgroundDuration)
                .From(leftOffscreenPosition));
            _activeAnimation.AppendCallback(() => particle.Play());
            
            packName.text = string.Empty;
            _activeAnimation.Append(packName.DOText(_packName, animatorConfig.AnimatorConfig.TextDuration));

            packProgress.SetProgressImmediately(_packProgress - 1, _packLevelCount);
            _activeAnimation.Append(packProgress.SetProgress(_packProgress, _packLevelCount));
            
            energyView.SetProgressImmediately(_startEnergy, _maxEnergy);
            energyView.SetTimeLeft(0);
            _activeAnimation.Append(energyView.transform
                .DOMoveX(center.x, animatorConfig.AnimatorConfig.EnergyShowDuration)
                .From(rightOffscreenPosition));
            _activeAnimation.Append(energyView.SetProgress(_startEnergy + _addedEnergy, _maxEnergy));
            
            if (_packName != _nextPackName)
            {
                _activeAnimation.Append(packInfoContainer.DOScale(0, animatorConfig.AnimatorConfig.ChangePackImageDuration));
                _activeAnimation.AppendCallback(() =>
                {
                    packName.text = _nextPackName;
                    packImage.sprite = _nextPackImage;
                    packProgress.SetProgressImmediately(0, _nextPackLevelCount);
                });
                _activeAnimation.Append(packInfoContainer.DOScale(1, animatorConfig.AnimatorConfig.ChangePackImageDuration));
            }
            
            _activeAnimation.AppendInterval(0);
            for (int i = 1; i <= buttons.Length; i++)
            {
                var button = buttons[i - 1];
                
                _activeAnimation.Join(button.transform
                    .DOMoveX(center.x, animatorConfig.AnimatorConfig.PopUpDuration + animatorConfig.AnimatorConfig.ItemMoveDelay * i)
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

            _activeAnimation.AppendInterval(animatorConfig.AnimatorConfig.HideDelay);

            _activeAnimation.Append(popUpImage.transform
                .DOMoveX(rightOffscreenPosition, animatorConfig.AnimatorConfig.PopUpDuration));

            var hideDelay = animatorConfig.AnimatorConfig.ItemMoveDelay;
            var popUpDuration = animatorConfig.AnimatorConfig.PopUpDuration;
            
            int i = 0;
            _activeAnimation.Join(energyView.transform
                .DOMoveX(rightOffscreenPosition, popUpDuration + hideDelay * ++i));
            _activeAnimation.Join(title.transform
                .DOMoveX(rightOffscreenPosition, popUpDuration + hideDelay * ++i));
            _activeAnimation.Join(packBackground.transform
                .DOMoveX(rightOffscreenPosition, popUpDuration + hideDelay * ++i));
            
            for (int j = 0; j < buttons.Length; j++, i++)
            {
                var button = buttons[j];
                
                _activeAnimation.Join(button.transform
                    .DOMoveX(rightOffscreenPosition, popUpDuration + hideDelay * i));
            }

            _activeAnimation.AppendCallback(() =>
                particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear));
            _activeAnimation.AppendCallback(() => OnAnimationHidden?.Invoke());
            _activeAnimation.SetEase(Ease.InQuad);
        }
    }
}