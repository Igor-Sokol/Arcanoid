using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Library.TimeManagers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private TimeScaler[] timeScales;

        public float DeltaTime => GetDeltaTime();
        public float Scale => GetScale();

        private float GetDeltaTime()
        {
            return Time.deltaTime * GetScale();
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