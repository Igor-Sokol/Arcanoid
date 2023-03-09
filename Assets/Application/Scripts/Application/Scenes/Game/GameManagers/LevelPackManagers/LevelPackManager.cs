using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Screen.UI.LevelPackViews;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Contracts;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Implementations;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;

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

        public void Initialize()
        {
            _packProgressManager = ProjectContext.Instance.GetService<IPackProgressManager>();
            var pack = ProjectContext.Instance.GetService<IPackInfo>() ?? defaultPackInfo;
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
        
        public bool TrySetNextLevel()
        {
            if (_currentLevelIndex + 1 >= _levels.Count)
            {
                if (_packProgressManager.TryGetPackIndex(_packInfo.LevelPack, out int currentIndex))
                {
                    var lastSavedPack = _packProgressManager.GetCurrentLevel();
                    _packProgressManager.TryGetPackIndex(lastSavedPack.LevelPack, out int savedIndex);

                    if (currentIndex + 1 == savedIndex)
                    {
                        LoadPack(lastSavedPack);
                        return true;
                    }
                }
                
                return false;
            }
            else
            {
                _currentLevelIndex++;
                return true;
            }
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