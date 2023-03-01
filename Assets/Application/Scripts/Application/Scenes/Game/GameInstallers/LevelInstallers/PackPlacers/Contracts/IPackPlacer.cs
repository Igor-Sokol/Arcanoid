using Application.Scripts.Application.Scenes.Game.Units.Blocks;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.LevelInstallers.PackPlacers.Contracts
{
    public interface IPackPlacer
    {
        void Place(Block[][] blocks);
    }
}