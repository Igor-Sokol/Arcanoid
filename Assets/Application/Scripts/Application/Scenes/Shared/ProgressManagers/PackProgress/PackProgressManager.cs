using System;
using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Shared.LevelManagement;
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
        [SerializeField] private PackManager levelPacks;

        public IEnumerable<LevelPack> LevelPacks => levelPacks.LevelPacks;

        public IPackInfo GetCurrentLevel()
        {
            var packTransfer = progressRepository.Load();


            if (packTransfer.PackIndex < levelPacks.LevelPacks.Length)
            {
                return new PackInfo(levelPacks.LevelPacks[packTransfer.PackIndex], packTransfer.CurrentLevelIndex);
            }

            return new PackInfo(null, 0);
        }

        public bool TryGetPackIndex(LevelPack pack, out int index)
        {
            if (levelPacks.LevelPacks.Contains(pack))
            {
                index = Array.IndexOf(levelPacks.LevelPacks, pack);
                return true;
            }

            index = -1;
            return false;
        }
        
        public void CompleteLevel(IPackInfo packInfo)
        {
            if (levelPacks.LevelPacks.Contains(packInfo.LevelPack))
            {
                int levelPackIndex = Array.IndexOf(levelPacks.LevelPacks, packInfo.LevelPack);
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