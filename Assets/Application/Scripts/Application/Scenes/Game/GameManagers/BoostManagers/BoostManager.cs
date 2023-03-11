using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.BoostActions;
using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
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
        private readonly List<ActionHandler> _boosts = new List<ActionHandler>();
        private IGameActionManager _gameActionManager;

        public void Initialize()
        {
            _gameActionManager = ProjectContext.Instance.GetService<IGameActionManager>();
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
            boost.Initialize();
            _boosts.RemoveAll(h => !h.Valid);
            var handler = _gameActionManager.StartAction(new BoostGameAction(boost), boost.Duration);
            _boosts.Add(handler);
        }

        public void PrepareReuse()
        {
            foreach (var handler in _boosts.Where(h => h.Valid))
            {
                handler.Stop();
            }
            _boosts.Clear();
        }
    }
}