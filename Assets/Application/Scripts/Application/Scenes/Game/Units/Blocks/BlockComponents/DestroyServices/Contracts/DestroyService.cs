using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Contracts
{
    public abstract class DestroyService : MonoBehaviour, IDestroyService
    {
        public abstract void Initialize();
        public abstract void PrepareReuse();
        public abstract void OnDestroyAction(Block block);
    }
}