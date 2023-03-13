using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices.Contracts
{
    public abstract class BlockService : MonoBehaviour, IBlockService
    {
        public abstract void PrepareReuse();
    }
}