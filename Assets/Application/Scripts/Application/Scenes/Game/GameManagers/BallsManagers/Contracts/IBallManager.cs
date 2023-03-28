using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.BallSetUpServices;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.BallSetUpServices.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Application.Scripts.Library.Reusable;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts
{
    public interface IBallManager : IReusable
    {
        int BallsCount { get; }
        IEnumerable<Ball> Balls { get; }
        IBallSetUpManager BallSetUpManager { get; }
        event Action OnAllBallRemoved;
        Ball GetBall();
        void ReturnBall(Ball ball);
    }
}