using System;
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
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;
using Application.Scripts.Library.SceneManagers.Contracts.SceneManagers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Application.Scripts.Application.Scenes.ChoosePack.Managers
{
    public class PackViewManager : MonoBehaviour, IInitializing
    {
        private readonly List<PackView> _packs = new List<PackView>();

        private IPackProgressManager _packProgressManager;
        private ISceneManager _sceneManager;
        private IEnergyManager _energyManager;
        private IPackInfo _packInfo;
        
        [SerializeField] private PackView packViewPrefab;
        [SerializeField] private Transform viewContainer;
        [FormerlySerializedAs("energyPrices")] [SerializeField] private EnergyPriceConfig energyPriceConfig;
        
        public void Initialize()
        {
            _packProgressManager = ProjectContext.Instance.GetService<IPackProgressManager>();
            _sceneManager = ProjectContext.Instance.GetService<ISceneManager>();
            _energyManager = ProjectContext.Instance.GetService<IEnergyManager>();

            GenerateViews();
            PackStateUpdate();
            _energyManager.OnEnergyAdded += PackStateUpdate;
            _energyManager.OnEnergyRemoved += PackStateUpdate;
        }

        private void OnDisable()
        {
            _energyManager.OnEnergyAdded -= PackStateUpdate;
            _energyManager.OnEnergyRemoved -= PackStateUpdate;
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

        private void LoadPack(PackView packView, LevelPack levelPack)
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
            
            _sceneManager.LoadScene<DefaultSceneLoading>(Scene.Game);
        }

        private void PackStateUpdate()
        {
            bool interactable = _energyManager.CurrentEnergy >= energyPriceConfig.LevelPrice;
            foreach (var pack in _packs.Where(p => p.State != PackState.Closed))
            {
                pack.Interactable = interactable;
            }
        }
    }
}