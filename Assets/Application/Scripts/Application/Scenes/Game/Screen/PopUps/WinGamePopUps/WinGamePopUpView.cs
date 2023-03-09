using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;
using Application.Scripts.Application.Scenes.Shared.UI.StringViews;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.Localization.LocalizationManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps
{
    public class WinGamePopUpView : MonoBehaviour
    {
        private ILocalizationManager _localizationManager;
        
        [SerializeField] private Image packImage;
        [SerializeField] private TMP_Text packName;
        [SerializeField] private ProgressView packProgress;

        public void Configure(IPackInfo packInfo)
        {
            _localizationManager ??= ProjectContext.Instance.GetService<ILocalizationManager>();
            
            packImage.sprite = packInfo.LevelPack.PackImage;
            packName.text = _localizationManager.GetString(packInfo.LevelPack.PackNameKey);
            packProgress.SetProgress(packInfo.CurrentLevelIndex + 1, packInfo.LevelPack.LevelCount);
        }
    }
}