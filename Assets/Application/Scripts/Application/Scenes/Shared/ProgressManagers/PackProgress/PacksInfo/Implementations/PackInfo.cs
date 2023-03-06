using Application.Scripts.Application.Scenes.Shared.LevelManagement.LevelPacks;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;

namespace Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Implementations
{
    public class PackInfo : IPackInfo
    {
        public LevelPack LevelPack { get; private set; }
        public int CurrentLevelIndex { get; private set; }

        public PackInfo(LevelPack levelPack, int currentLevelIndex)
        {
            LevelPack = levelPack;
            CurrentLevelIndex = currentLevelIndex;
        }
    }
}