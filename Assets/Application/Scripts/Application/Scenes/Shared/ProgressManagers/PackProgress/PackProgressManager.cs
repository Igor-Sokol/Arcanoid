using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.LevelPacks;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Contracts;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Implementations;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.ProgressObjects;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Repository.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress
{
    public class PackProgressManager : MonoBehaviour, IPackProgressManager
    {
        [SerializeField] private PackProgressRepository progressRepository;
        [SerializeField] private List<LevelPack> levelPacks;

        public IEnumerable<LevelPack> LevelPacks => levelPacks;

        public IPackInfo GetCurrentLevel()
        {
            var packTransfer = progressRepository.Load();
            return new PackInfo(levelPacks[packTransfer.PackIndex], packTransfer.CurrentLevelIndex);
        }

        public void CompleteLevel(IPackInfo packInfo)
        {
            if (levelPacks.Contains(packInfo.LevelPack))
            {
                int levelPackIndex = levelPacks.IndexOf(packInfo.LevelPack);
                var packTransfer = progressRepository.Load();

                if (levelPackIndex >= packTransfer.PackIndex)
                {
                    if (packInfo.CurrentLevelIndex >= packInfo.LevelPack.LevelCount - 1) 
                    {
                        var transfer = new PackProgressTransfer(levelPackIndex + 1, 0);
                        progressRepository.Save(transfer);
                    }
                    else if (packInfo.CurrentLevelIndex >= packTransfer.CurrentLevelIndex)
                    {
                        var transfer = new PackProgressTransfer(levelPackIndex,
                            packInfo.CurrentLevelIndex + 1);
                        progressRepository.Save(transfer);
                    }
                }
            }
        }
    }
}