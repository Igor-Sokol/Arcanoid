using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.BallSetUpServices;
using Application.Scripts.Application.Scenes.Game.Units.Balls;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts
{
    public interface IBallManager
    {
        int BallsCount { get; }
        IEnumerable<Ball> Balls { get; }
        BallSetUpManager BallSetUpManager { get; }
        event Action OnAllBallRemoved;
        Ball GetBall();
        void ReturnBall(Ball ball);
    }
}