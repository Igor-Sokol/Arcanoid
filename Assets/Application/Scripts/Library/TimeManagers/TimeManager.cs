using System.Collections.Generic;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Library.TimeManagers
{
    public class TimeManager : MonoBehaviour
    {
        private List<ITimeScaler> _timeScales;

        public IEnumerable<ITimeScaler> TimeScales => _timeScales; 
        public float DeltaTime => GetDeltaTime();
        public float Scale => GetScale();

        public void AddTimeScaler(ITimeScaler scaler)
        {
            _timeScales.Add(scaler);
        }

        public void RemoveTimeScaler(ITimeScaler scaler)
        {
            _timeScales.Remove(scaler);
        }
        
        private float GetDeltaTime()
        {
            return Time.deltaTime * GetScale();
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