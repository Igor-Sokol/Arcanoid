using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Contracts
{
    public abstract class HitService : MonoBehaviour, IHitService
    {
        public abstract void OnHitAction(Collision2D col);
        public abstract void PrepareReuse();
    }
}