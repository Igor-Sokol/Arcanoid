using System;
using System.Collections.Generic;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Library.TimeManagers
{
    public class TimeScaleManager : MonoBehaviour
    {
        private Dictionary<Type, ITimeScaler> _timeScales;

        [SerializeField] private List<TimeScaler> timeScales;

        private void Awake()
        {
            foreach (var timeScale in timeScales)
            {
                _timeScales.Add(timeScale.GetType(), timeScale);
            }
        }

        public ITimeScaler GetTimeScale<T>()
            where T : ITimeScaler
        {
            if (_timeScales.TryGetValue(typeof(T), out var scale))
            {
                return scale;
            }

            return null;
        }
    }
}