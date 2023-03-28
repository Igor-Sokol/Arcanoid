using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.BallSetUpServices.Contracts;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.BallSetUpServices
{
    public class BallSetUpManager : IBallSetUpManager
    {
        private readonly Dictionary<Type, IBallSetUpAction> _ballActions = new Dictionary<Type, IBallSetUpAction>();

        public IEnumerable<IBallSetUpAction> BallSetUpActions => _ballActions.Values;
        public event Action<IBallSetUpAction> OnActionAdded;
        public event Action<IBallSetUpAction> OnActionRemoved;

        public void AddAction(IBallSetUpAction setUpAction)
        {
            var type = setUpAction.GetType();
            if (!_ballActions.ContainsKey(type))
            {
                _ballActions.Add(type, setUpAction);
                OnActionAdded?.Invoke(setUpAction);
            }
        }

        public void RemoveAction(Type actionType)
        {
            if (_ballActions.TryGetValue(actionType, out var action))
            {
                _ballActions.Remove(actionType);
                OnActionRemoved?.Invoke(action);
            }
        }

        public void PrepareReuse()
        {
            _ballActions.Clear();
        }
    }
}