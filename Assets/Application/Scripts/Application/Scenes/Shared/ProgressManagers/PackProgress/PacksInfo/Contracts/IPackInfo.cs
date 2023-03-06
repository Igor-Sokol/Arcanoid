using Application.Scripts.Application.Scenes.Shared.LevelManagement.LevelPacks;

namespace Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts
{
    public interface IPackInfo
    {
        LevelPack LevelPack { get; }
        int CurrentLevelIndex { get; }
    }
}