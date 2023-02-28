using UnityEngine;

namespace Application.Scripts.Library.TimeManagers.Contracts
{
    public abstract class TimeScaler : MonoBehaviour, ITimeScaler
    {
        public abstract float Scale { get; set; }
    }
}