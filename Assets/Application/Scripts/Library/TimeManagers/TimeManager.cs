using System.Collections.Generic;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Library.TimeManagers
{
    public class TimeManager
    {
        private readonly List<TimeScaler> _timeScales = new List<TimeScaler>();

        public IEnumerable<TimeScaler> TimeScales => _timeScales;
        public float DeltaTime => Time.deltaTime * Scale;
        public float FixedDeltaTime => Time.fixedDeltaTime * Scale;
        public float Scale => GetScale();

        public void AddTimeScaler(TimeScaler scaler)
        {
            _timeScales.Add(scaler);
        }

        public void RemoveTimeScaler(TimeScaler scaler)
        {
            _timeScales.Remove(scaler);
        }

        public void ClearTimeScales()
        {
            _timeScales.Clear();
        }

        private float GetScale()
        {
            float scale = 1f;

            foreach (var scaler in _timeScales)
            {
                scale *= scaler.Scale;
            }

            return scale;
        }
    }
}