using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Library.TimeManagers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private TimeScaler[] timeScales;

        public float DeltaTime => GetDeltaTime();

        private float GetDeltaTime()
        {
            float scale = 1f;

            foreach (var scaler in timeScales)
            {
                scale *= scaler.Scale;
            }
            
            return Time.deltaTime * scale;
        }
    }
}