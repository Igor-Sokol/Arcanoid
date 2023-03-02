using System.Collections.Generic;
using System.Linq;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using UnityEngine;

namespace Application.Scripts.Library.GameActionManagers
{
    public class GameActionManager : MonoBehaviour
    {
        private readonly List<ActionTimer> _timers = new List<ActionTimer>();

        private void Update()
        {
            foreach (var timer in _timers)
            {
                timer.Update(Time.deltaTime);
            }
        }

        public ActionHandler StartAction(IGameAction gameAction, float time, IActionTimeScale timeScale = null)
        {
            ActionTimer counter = GetTimer();
            return counter.Start(gameAction, time, timeScale);
        }

        public IEnumerable<ActionHandler> GetGameAction<T>() 
            where T : IGameAction
        {
            return _timers.Where(t => t.ActionHandler.GetType() == typeof(T)).Select(t => t.ActionHandler);
        }
        
        private ActionTimer GetTimer()
        {
            ActionTimer counter = null;

            foreach (var timerCounter in _timers.Where(timerCounter => !timerCounter.Active))
            {
                counter = timerCounter;
            }

            return counter ??= CreateTimer();
        }
        
        private ActionTimer CreateTimer()
        {
            ActionTimer counter = new ActionTimer();
            _timers.Add(counter);

            return counter;
        }
    }
}