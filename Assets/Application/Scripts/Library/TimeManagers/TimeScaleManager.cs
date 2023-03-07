using System;
using System.Collections.Generic;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Library.TimeManagers
{
    public class TimeScaleManager : MonoBehaviour, ITimeScaleManager
    {
        private readonly Dictionary<Type, TimeScaler> _timeScales = new Dictionary<Type, TimeScaler>();

        [SerializeField] private List<TimeScaler> timeScales;

        private void Awake()
        {
            foreach (var timeScale in timeScales)
            {
                _timeScales.Add(timeScale.GetType(), timeScale);
            }
        }

        public T GetTimeScale<T>()
            where T : TimeScaler
        {
            if (_timeScales.TryGetValue(typeof(T), out var scale))
            {
                return (T)scale;
            }

            return null;
        }
    }
}