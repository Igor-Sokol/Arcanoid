using System;
using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using UnityEngine;

namespace Application.Scripts.Library.GameActionManagers
{
    public class GameActionManager : MonoBehaviour
    {
        private readonly Dictionary<Type, List<ActionTimer>> _timers = new Dictionary<Type, List<ActionTimer>>();

        private void Update()
        {
            foreach (var timers in _timers)
            {
                foreach (var timer in timers.Value)
                {
                    timer.Update(Time.deltaTime);
                }
            }
        }

        public ActionHandler StartAction(IGameAction gameAction, float time, IActionTimeScale timeScale = null)
        {
            ActionTimer counter = GetTimer<IGameAction>();
            return counter.Start(gameAction, time, timeScale);
        }

        public IEnumerable<ActionHandler> GetGameAction<T>() 
            where T : IGameAction
        {
            if (_timers.TryGetValue(typeof(T), out List<ActionTimer> timers))
            {
                return timers.Where(t => t.GameAction != null).Select(t => t.ActionHandler);
            }

            return null;
        }
        
        private ActionTimer GetTimer<T>() 
            where T : IGameAction
        {
            ActionTimer counter = null;
            
            if (_timers.TryGetValue(typeof(T), out List<ActionTimer> timers))
            {
                counter = timers.FirstOrDefault(t => !t.Active);
            }

            return counter ??= CreateTimer<T>();
        }
        
        private ActionTimer CreateTimer<T>() 
            where T : IGameAction
        {
            ActionTimer counter = new ActionTimer();

            Type key = typeof(T);
            if (_timers.ContainsKey(key))
            {
                _timers[key].Add(counter);
            }
            else
            {
                _timers.Add(key, new List<ActionTimer>() { counter });
            }

            return counter;
        }
    }
}