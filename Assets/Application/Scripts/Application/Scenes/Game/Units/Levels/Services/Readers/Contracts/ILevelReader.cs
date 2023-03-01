namespace Application.Scripts.Application.Scenes.Game.Units.Levels.Services.Readers.Contracts
{
    public interface ILevelReader
    {
        string[][] ReadPack(LevelInfo level);
    }
}