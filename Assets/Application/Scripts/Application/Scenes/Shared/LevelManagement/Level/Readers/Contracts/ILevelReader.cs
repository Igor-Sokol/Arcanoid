namespace Application.Scripts.Application.Scenes.Shared.LevelManagement.Level.Readers.Contracts
{
    public interface ILevelReader
    {
        string[][] ReadPack(LevelInfo level);
    }
}