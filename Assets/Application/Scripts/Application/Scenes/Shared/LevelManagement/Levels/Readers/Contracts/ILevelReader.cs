namespace Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.Contracts
{
    public interface ILevelReader
    {
        string[][] ReadPack(LevelInfo level);
    }
}