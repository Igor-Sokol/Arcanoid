using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices.Implementation;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Implementations
{
    public class BlockHealthService : HitService
    {
        [SerializeField] private BlockHealth blockHealth;

        public override void OnHitAction()
        {
            blockHealth.RemoveHealth();
        }

        public override void PrepareReuse()
        {
        }
    }
}