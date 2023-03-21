using Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Contracts;

namespace Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.Contracts
{
    public interface ILevelReader
    {
        BlockInfo[][] ReadPack(LevelInfo level);
    }
}