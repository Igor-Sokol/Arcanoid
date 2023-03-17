using Application.Scripts.Application.Scenes.Game.Units.Balls;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.BallSetUpServices.Contracts
{
    public interface IBallSetUpAction
    {
        void Install(Ball ball);
        void UnInstall(Ball ball);
    }
}