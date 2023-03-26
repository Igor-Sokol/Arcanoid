using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Screen.UI.LevelPackViews;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Contracts;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Implementations;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.LevelPackManagers
{
    public class LevelPackManager : MonoBehaviour, IInitializing
    {
        private IPackProgressManager _packProgressManager;
        private IPackInfo _packInfo;
        private List<LevelInfo> _levels;
        private int _currentLevelIndex;

        [SerializeField] private PackInfoMono defaultPackInfo;
        [SerializeField] private LevelPackView levelPackView;

        public int CurrentLevelIndex => _currentLevelIndex;
        public int LevelsCount => _levels.Count;

        [Inject]
        private void Construct(IPackProgressManager packProgressManager)
        {
            _packProgressManager = packProgressManager;
        }
        
        public void Initialize()
        {
            var pack = _packProgressManager.SelectedPackInfo ?? defaultPackInfo;
            LoadPack(pack);
            RenderView();
        }

        public LevelInfo GetCurrentLevel()
        {
            return _currentLevelIndex >= _levels.Count ? _levels[_levels.Count - 1] : _levels[_currentLevelIndex];
        }

        public IPackInfo GetCurrentPackInfo()
        {
            return new PackInfo(_packInfo.LevelPack, _currentLevelIndex);
        }

        public bool NextLevelExists()
        {
            if (_currentLevelIndex + 1 >= _levels.Count)
            {
                if (_packProgressManager.TryGetPackIndex(_packInfo.LevelPack, out int currentIndex))
                {
                    var lastSavedPack = _packProgressManager.GetCurrentLevel();
                    _packProgressManager.TryGetPackIndex(lastSavedPack.LevelPack, out int savedIndex);

                    if (currentIndex + 1 == savedIndex && lastSavedPack.CurrentLevelIndex == 0)
                    {
                        return true;
                    }
                }
                
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public bool TrySetNextLevel()
        {
            if (NextLevelExists())
            {
                if (_currentLevelIndex + 1 >= _levels.Count)
                {
                    var lastSavedPack = _packProgressManager.GetCurrentLevel();
                    LoadPack(lastSavedPack);
                }
                else
                {
                    _currentLevelIndex++;
                }

                return true;
            }

            return false;
        }

        private void LoadPack(IPackInfo packInfo)
        {
            _packInfo = packInfo;
            _levels = new List<LevelInfo>(_packInfo.LevelPack.Levels);
            _currentLevelIndex = _packInfo.CurrentLevelIndex;
        }
        
        public void RenderView()
        {
            levelPackView.PackProgress.SetProgress(_currentLevelIndex + 1, _packInfo.LevelPack.LevelCount);
            levelPackView.PackImage = _packInfo.LevelPack.PackImage;
        }
    }
}