using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Contracts
{
    public abstract class HitService : MonoBehaviour, IHitService
    {
        public abstract void OnHitAction();
        public abstract void PrepareReuse();
    }
}