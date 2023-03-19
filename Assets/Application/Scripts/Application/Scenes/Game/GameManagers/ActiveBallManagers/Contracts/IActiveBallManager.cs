using Application.Scripts.Application.Scenes.Game.Units.Balls;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.ActiveBallManagers.Contracts
{
    public interface IActiveBallManager
    {
        Ball GetBall(string key);
        void Return(Ball ball);
    }
}