using System;
using Application.Scripts.Application.Scenes.ChoosePack.LevelPacks.Components;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.LevelPacks;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Localization.LocalizationManagers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.ChoosePack.LevelPacks
{
    public class PackView : MonoBehaviour, IInitializing
    {
        private LocalizationManager _localizationManager;
        private LevelPack _levelPack;
        
        [SerializeField] private Image packBackground;
        [SerializeField] private Image packImage;
        [SerializeField] private TMP_Text packName;
        [FormerlySerializedAs("progress")] [SerializeField] private ProgressView progressView;
        [SerializeField] private Button button;
        [SerializeField] private Sprite closedBackground;
        [SerializeField] private Sprite currentBackground;
        [SerializeField] private Sprite completedBackground;
        [SerializeField] private Sprite closePackImage;
        [SerializeField] private string closePackNameKey;

        public event Action<PackView, LevelPack> OnSelected;

        public void Initialize()
        {
            _localizationManager = ProjectContext.Instance.GetService<LocalizationManager>();
        }
        
        public void Configure(LevelPack levelPack, PackState packState, int completedLevel)
        {
            Initialize();
            _levelPack = levelPack;

            ConfigureState(levelPack, packState);
            ConfigureBackground(packState);
            ConfigureProgress(levelPack, completedLevel);
        }

        private void ConfigureState(LevelPack levelPack, PackState packState)
        {
            switch (packState)
            {
                case PackState.Closed:
                {
                    packImage.sprite = closePackImage;
                    packName.text = _localizationManager.GetString(closePackNameKey);
                    button.interactable = false;
                }
                    break;
                case PackState.Current:
                case PackState.Completed:
                {
                    packImage.sprite = levelPack.PackImage;
                    packName.text = _localizationManager.GetString(levelPack.PackNameKey);
                }
                    break;
            }
        }

        private void ConfigureProgress(LevelPack levelPack, int completedLevel)
        {
            progressView.SetProgress(completedLevel, levelPack.LevelCount);
        }
        
        private void ConfigureBackground(PackState packState)
        {
            switch (packState)
            {
                case PackState.Closed:
                {
                    packBackground.sprite = closedBackground;
                }
                    break;
                case PackState.Current:
                {
                    packBackground.sprite = currentBackground;
                }
                    break;
                case PackState.Completed:
                {
                    packBackground.sprite = completedBackground;
                }
                    break;
            }
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            OnSelected?.Invoke(this, _levelPack);
        }
    }
}