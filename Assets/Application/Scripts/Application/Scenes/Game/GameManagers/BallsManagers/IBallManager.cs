using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Units.Balls;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers
{
    public interface IBallManager
    {
        int BallsCount { get; }
        IEnumerable<Ball> Balls { get; }
        event Action OnAllBallRemoved;
        Ball GetBall();
        void ReturnBall(Ball ball);
    }
}