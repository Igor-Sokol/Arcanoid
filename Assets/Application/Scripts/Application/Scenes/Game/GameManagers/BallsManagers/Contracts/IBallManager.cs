using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Contracts;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts
{
    public interface IBallManager
    {
        int BallsCount { get; }
        IEnumerable<Ball> Balls { get; }
        event Action OnAllBallRemoved;
        void AddBallService<T>(T service) where T : IBallHitService;
        void RemoveBallService<T>(T service) where T : IBallHitService;
        Ball GetBall();
        void ReturnBall(Ball ball);
    }
}