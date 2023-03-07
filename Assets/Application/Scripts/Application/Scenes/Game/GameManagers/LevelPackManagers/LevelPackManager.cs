using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Screen.UI.LevelPackViews;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Implementations;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.LevelPackManagers
{
    public class LevelPackManager : MonoBehaviour, IInitializing
    {
        private IPackInfo _packInfo;
        private List<LevelInfo> _levels;
        private int _currentLevelIndex;

        [SerializeField] private PackInfoMono defaultPackInfo;
        [SerializeField] private LevelPackView levelPackView;
        
        public void Initialize()
        {
            _packInfo = ProjectContext.Instance.GetService<IPackInfo>() ?? defaultPackInfo;
            _levels = new List<LevelInfo>(_packInfo.LevelPack.Levels);
            _currentLevelIndex = _packInfo.CurrentLevelIndex;

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
                return false;
            }
            else
            {
                _currentLevelIndex++;
                return true;
            }
        }

        private void RenderView()
        {
            levelPackView.PackProgress.SetProgress(_currentLevelIndex, _packInfo.LevelPack.LevelCount);
            levelPackView.LevelProgress.SetProgress(0, 0);
            levelPackView.PackImage = _packInfo.LevelPack.PackImage;
        }
    }
}