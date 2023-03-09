using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.LevelPacks;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;

namespace Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Contracts
{
    public interface IPackProgressManager
    {
        IEnumerable<LevelPack> LevelPacks { get; }
        IPackInfo GetCurrentLevel();
        void CompleteLevel(IPackInfo packInfo);
        bool TryGetPackIndex(LevelPack pack, out int index);
    }
}