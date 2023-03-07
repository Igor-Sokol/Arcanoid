using System.Collections.Generic;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Application.Scripts.Library.TimeManagers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private List<TimeScaler> timeScales;

        public IEnumerable<TimeScaler> TimeScales => timeScales;
        public float DeltaTime => Time.deltaTime * Scale;
        public float FixedDeltaTime => Time.fixedDeltaTime * Scale;
        public float Scale => GetScale();

        public void AddTimeScaler(TimeScaler scaler)
        {
            timeScales.Add(scaler);
        }

        public void RemoveTimeScaler(TimeScaler scaler)
        {
            timeScales.Remove(scaler);
        }

        public void ClearTimeScales()
        {
            timeScales.Clear();
        }

        private float GetScale()
        {
            float scale = 1f;

            foreach (var scaler in timeScales)
            {
                scale *= scaler.Scale;
            }

            return scale;
        }
    }
}