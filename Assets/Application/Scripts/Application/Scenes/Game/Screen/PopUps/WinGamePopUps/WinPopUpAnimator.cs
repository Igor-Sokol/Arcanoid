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

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps
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
        
        [SerializeField] private Image popUp;
        [SerializeField] private Vector2 popUpPositionOffset;
        [SerializeField] private TMP_Text title;
        [SerializeField] private EnergyView energyView;
        [SerializeField] private Image packBackground;
        [SerializeField] private Image packImage;
        [SerializeField] private TMP_Text packName;
        [SerializeField] private ProgressView packProgress;
        [SerializeField] private Button[] buttons;
        [SerializeField] private float popUpDuration;
        [SerializeField] private float textDuration;
        [SerializeField] private float energyShowDuration;
        [SerializeField] private float addedEnergyInterval;
        [SerializeField] private float packBackgroundDuration;
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

            packImage.sprite = _packImage;
            _activeAnimation.Append(packBackground.transform
                .DOMoveX(center.x, packBackgroundDuration)
                .From(leftOffscreenPosition));
            
            packName.text = string.Empty;
            _activeAnimation.Append(packName.DOText(_packName, textDuration));

            packProgress.SetProgressImmediately(_packProgress - 1, _packLevelCount);
            _activeAnimation.Append(packProgress.SetProgress(_packProgress, _packLevelCount));
            
            energyView.SetProgressImmediately(0, _maxEnergy);
            _activeAnimation.Append(energyView.transform
                .DOMoveX(center.x, energyShowDuration)
                .From(rightOffscreenPosition));
            _activeAnimation.Append(energyView.SetProgress(_startEnergy, _maxEnergy));
            _activeAnimation.AppendInterval(addedEnergyInterval);
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