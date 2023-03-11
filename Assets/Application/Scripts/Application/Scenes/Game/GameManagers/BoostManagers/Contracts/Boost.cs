using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts
{
    public abstract class Boost : MonoBehaviour, IBoost
    {
        public abstract void Initialize();
        public abstract float Duration { get; }
        public abstract void Enable();
        public abstract void Disable();
    }
}