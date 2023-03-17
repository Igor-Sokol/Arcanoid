using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallEffectManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Contracts;
using Application.Scripts.Library.TimeManagers;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts
{
    public interface IBallManager
    {
        int BallsCount { get; }
        IEnumerable<Ball> Balls { get; }
        event Action OnAllBallRemoved;
        void AddBallEffect<T>(T effect) where T : BallEffect;
        void RemoveBallEffect<T>(T effect) where T : BallEffect;
        void AddBallService<T>(T service) where T : IBallHitService;
        void RemoveBallService<T>(T service) where T : IBallHitService;
        Ball GetBall();
        void ReturnBall(Ball ball);
    }
}