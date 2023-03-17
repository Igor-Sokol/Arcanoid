using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallEffectManagers.Contracts
{
    public abstract class BallEffect : MonoBehaviour, IBallEffect
    {
        public abstract void DestroyAction();
    }
}