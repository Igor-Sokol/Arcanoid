using System;
using System.Collections.Generic;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.BoostActions;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Application.Scenes.Shared.LibraryImplementations.TimeManagers;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers
{
    public class BoostManager : MonoBehaviour, IBoostManager, IInitializing, IReusable
    {
        private readonly Dictionary<Type, List<ActionHandler>> _boosts = new Dictionary<Type, List<ActionHandler>>();
        private IGameActionManager _gameActionManager;

        [SerializeField] private ActionTimeManager actionTimeManager;

        public void Initialize()
        {
            _gameActionManager = ProjectContext.Instance.GetService<IGameActionManager>();
        }
        
        public void PrepareReuse()
        {
            foreach (var boosts in _boosts)
            {
                foreach (var boost in boosts.Value)
                {
                    boost.Stop();
                }
                boosts.Value.Clear();
            }
            _boosts.Clear();
        }
        
        public void Execute<T>(IEnumerable<T> boosts) 
            where T : IBoost
        {
            foreach (var boost in boosts)
            {
                Execute(boost);
            }
        }
        
        public void Execute<T>(T boost)
            where T : IBoost
        {
            ClearInvalid();
            
            boost.Initialize();
            var handler = _gameActionManager.StartAction(new BoostGameAction(boost), boost.Duration, actionTimeManager);

            var type = boost.GetType();
            if (_boosts.TryGetValue(type, out var list))
            {
                list.Add(handler);
            }
            else
            {
                _boosts.Add(type,  new List<ActionHandler>() { handler });
            }
        }

        public IEnumerable<ActionHandler> GetActiveBoost<T>()
        {
            ClearInvalid();

            if (_boosts.TryGetValue(typeof(T), out var list))
            {
                return list;
            }

            return null;
        }

        private void ClearInvalid()
        {
            foreach (var boosts in _boosts)
            {
                boosts.Value.RemoveAll(h => !h.Valid);
            }
        }
    }
}