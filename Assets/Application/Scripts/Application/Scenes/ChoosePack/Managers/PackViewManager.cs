using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.ChoosePack.LevelPacks;
using Application.Scripts.Application.Scenes.ChoosePack.LevelPacks.Components;
using Application.Scripts.Application.Scenes.Shared.Energy.Config;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.LevelPacks;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.SceneManagers.Loading;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Contracts;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Implementations;
using Application.Scripts.Application.Scenes.Shared.UI.Header;
using Application.Scripts.Application.Scenes.Shared.UI.PopUps.MessagePopUp;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Localization.LocalizationManagers;
using Application.Scripts.Library.PopUpManagers;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.ChoosePack.Managers
{
    public class PackViewManager : MonoBehaviour, IInitializing
    {
        private readonly List<PackView> _packs = new List<PackView>();

        private IPackProgressManager _packProgressManager;
        private ISceneManager _sceneManager;
        private IEnergyManager _energyManager;
        private IPackInfo _packInfo;
        private IPopUpManager _popUpManager;
        private ILocalizationManager _localizationManager;
        private Tween _activeAnimation;
        
        [SerializeField] private PackView packViewPrefab;
        [SerializeField] private RectTransform viewContainer;
        [SerializeField] private RectTransform viewport;
        [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
        [SerializeField] private PackViewAnimator packViewAnimator;
        [SerializeField] private EnergyValueConfig energyPriceConfig;
        [SerializeField] private Image graphicRayBlock;
        [SerializeField] private float scrollTime;
        [SerializeField] private string noEnergyKey;
        
        public void Initialize()
        {
            _packProgressManager = ProjectContext.Instance.GetService<IPackProgressManager>();
            _sceneManager = ProjectContext.Instance.GetService<ISceneManager>();
            _popUpManager = ProjectContext.Instance.GetService<IPopUpManager>();
            _energyManager = ProjectContext.Instance.GetService<IEnergyManager>();
            _localizationManager = ProjectContext.Instance.GetService<ILocalizationManager>();

            GenerateViews();
            SetViewsPosition();
            PackStateUpdate();
            
            _energyManager.OnEnergyAdded += PackStateUpdate;
            _energyManager.OnEnergyRemoved += PackStateUpdate;
            
            packViewAnimator.Show(_packs);
        }

        private void OnEnable()
        {
            if (_energyManager != null)
            {
                _energyManager.OnEnergyAdded += PackStateUpdate;
                _energyManager.OnEnergyRemoved += PackStateUpdate;
            }
        }

        private void OnDisable()
        {
            if (_energyManager != null)
            {
                _energyManager.OnEnergyAdded -= PackStateUpdate;
                _energyManager.OnEnergyRemoved -= PackStateUpdate;
            }
            
            _activeAnimation?.Kill();
        }

        private void GenerateViews()
        {
            _packInfo = _packProgressManager.GetCurrentLevel();

            PackState packState = PackState.Completed;
            foreach (var levelPack in _packProgressManager.LevelPacks)
            {
                if (levelPack == _packInfo.LevelPack) packState = PackState.Current;

                var packView = Instantiate(packViewPrefab, viewContainer);
                _packs.Add(packView);
                packView.transform.SetSiblingIndex(0);
                packView.OnSelected += LoadPack;
                
                switch (packState)
                {
                    case PackState.Completed:
                        packView.Configure(levelPack, packState, levelPack.LevelCount, energyPriceConfig.LevelPrice);
                        break;
                    case PackState.Current:
                        packView.Configure(levelPack, packState, _packInfo.CurrentLevelIndex, energyPriceConfig.LevelPrice);
                        break;
                    case PackState.Closed:
                        packView.Configure(levelPack, packState, 0, energyPriceConfig.LevelPrice);
                        break;
                }

                if (levelPack == _packInfo.LevelPack) packState = PackState.Closed;
            }
        }

        private void SetViewsPosition()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(viewContainer);
            
            float viewSize = verticalLayoutGroup.preferredHeight / _packs.Count;
            float position = -verticalLayoutGroup.preferredHeight;
            _packProgressManager.TryGetPackIndex(_packInfo.LevelPack, out var currentIndex);
            position += viewSize * (_packs.Count - currentIndex);
            var viewportRect = viewport.rect;
            position -= viewportRect.height / 2f;
            position -= viewSize / 2;

            position = -Mathf.Clamp(Mathf.Abs(position), viewportRect.size.y, verticalLayoutGroup.preferredHeight);
            
            Transform viewTransform = viewContainer.transform;
            _activeAnimation = DOTween.To(() => viewTransform.localPosition.y,
                (y) => viewTransform.localPosition = new Vector3(viewTransform.localPosition.x, y), position,
                scrollTime);
        }

        private void LoadPack(PackView packView, LevelPack levelPack)
        {
            if (_energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice)
            {
                packView.OnSelected -= LoadPack;

                if (levelPack == _packInfo.LevelPack)
                {
                    ProjectContext.Instance.SetService<IPackInfo, IPackInfo>(_packInfo);
                }
                else
                {
                    ProjectContext.Instance.SetService<IPackInfo, PackInfo>(new PackInfo(levelPack, 0));
                }
            
                packViewAnimator.Hide(packView);
                graphicRayBlock.enabled = true;
                packViewAnimator.OnAnimationHidden += () => _sceneManager.LoadScene(Scene.Game);
            }
            else
            {
                ShowNoEnergyPopUp();
            }
        }

        private void PackStateUpdate()
        {
            // bool interactable = _energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice;
            // foreach (var pack in _packs.Where(p => p.State != PackState.Closed))
            // {
            //     pack.Interactable = interactable;
            // }
        }
        
        private void ShowNoEnergyPopUp()
        {
            var messagePopup = _popUpManager.Get<MessagePopUp>();
            messagePopup.Configure(_localizationManager.GetString(noEnergyKey));

            messagePopup.OnContinueSelected += messagePopup.Hide;
            
            messagePopup.Show();
        }
    }
}