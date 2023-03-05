using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels;
using Application.Scripts.Library.SceneManagers.Contracts.SceneInfo;

namespace Application.Scripts.Application.Scenes.Game.GameInstallers.SceneManagers.SceneInfo
{
    public class GameSceneArgs : SceneArgs
    {
        public LevelInfo LevelInfo { get; private set; }

        public GameSceneArgs(LevelInfo levelInfo)
        {
            LevelInfo = levelInfo;
        }
    }
}