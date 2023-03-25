using System;
using Application.Scripts.Application.Scenes.ChoosePack.LevelPacks.Components;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.LevelPacks;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.Localization.LocalizeEvents;
using Application.Scripts.Application.Scenes.Shared.UI.EnergyViews;
using Application.Scripts.Application.Scenes.Shared.UI.StringViews;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Localization.LocalizationManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ProjectContext = Application.Scripts.Library.DependencyInjection.ProjectContext;

namespace Application.Scripts.Application.Scenes.ChoosePack.LevelPacks
{
    public class PackView : MonoBehaviour
    {
        private ILocalizationManager _localizationManager;
        private LevelPack _levelPack;
        private PackState _packState;

        [SerializeField] private RectTransform packContainer;
        [SerializeField] private Image packBackground;
        [SerializeField] private Image packImage;
        [SerializeField] private TMP_Text packName;
        [SerializeField] private ProgressView progressView;
        [SerializeField] private Button button;
        [SerializeField] private Sprite closedBackground;
        [SerializeField] private Sprite currentBackground;
        [SerializeField] private Sprite completedBackground;
        [SerializeField] private Sprite closePackImage;
        [SerializeField] private string closePackNameKey;
        [SerializeField] private EnergyPriceView priceView;
        [SerializeField] private LocalizeString galaxy;

        public bool Interactable { get => button.interactable; set => button.interactable = value; }

        public RectTransform PackContainer => packContainer;
        public Vector2 Size => packBackground.sprite.rect.size;

        public PackState State => _packState;
        public event Action<PackView, LevelPack> OnSelected;

        [Inject]
        public void Construct(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
            galaxy.Construct(localizationManager);
        }

        public void Configure(LevelPack levelPack, PackState packState, int completedLevel, int price)
        {
            _packState = packState;
            _levelPack = levelPack;
            priceView.SetPrice(price);

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
                    priceView.gameObject.SetActive(false);
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