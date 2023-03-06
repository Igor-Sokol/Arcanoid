using System;
using Application.Scripts.Application.Scenes.ChoosePack.Packs;
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

namespace Application.Scripts.Application.Scenes.ChoosePack.Managers
{
    public class PackViewManager : MonoBehaviour, IInitializing
    {
        private IPackProgressManager _packProgressManager;
        private ISceneManager _sceneManager;
        private IPackInfo _packInfo;
        
        [SerializeField] private PackView packViewPrefab;
        [SerializeField] private Transform viewContainer;
        
        public void Initialize()
        {
            _packProgressManager = ProjectContext.Instance.GetService<IPackProgressManager>();
            _sceneManager = ProjectContext.Instance.GetService<ISceneManager>();

            GenerateViews();
        }

        private void GenerateViews()
        {
            _packInfo = _packProgressManager.GetCurrentLevel();

            PackState packState = PackState.Completed;
            foreach (var levelPack in _packProgressManager.LevelPacks)
            {
                if (levelPack == _packInfo.LevelPack) packState = PackState.Current;

                var packView = Instantiate(packViewPrefab, viewContainer);
                packView.transform.SetSiblingIndex(0);
                packView.OnSelected += LoadPack;
                
                switch (packState)
                {
                    case PackState.Completed:
                        packView.Configure(levelPack, packState, levelPack.LevelCount);
                        break;
                    case PackState.Current:
                        packView.Configure(levelPack, packState, _packInfo.CurrentLevelIndex);
                        break;
                    case PackState.Closed:
                        packView.Configure(levelPack, packState, 0);
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
    }
}