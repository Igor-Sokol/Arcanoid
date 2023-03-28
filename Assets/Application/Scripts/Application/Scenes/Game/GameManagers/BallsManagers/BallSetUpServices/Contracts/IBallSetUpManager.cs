using System;
using System.Collections.Generic;
using Application.Scripts.Library.Reusable;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.BallSetUpServices.Contracts
{
    public interface IBallSetUpManager : IReusable
    {
        IEnumerable<IBallSetUpAction> BallSetUpActions { get; }
        event Action<IBallSetUpAction> OnActionAdded;
        event Action<IBallSetUpAction> OnActionRemoved;
        void AddAction(IBallSetUpAction setUpAction);
        void RemoveAction(Type actionType);
    }
}