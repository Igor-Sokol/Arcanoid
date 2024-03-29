using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices.Implementation;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Implementations
{
    public class BlockHealthService : HitService
    {
        [SerializeField] private BlockHealth blockHealth;

        public override void OnHitAction(Collision2D col)
        {
            blockHealth.RemoveHealth();
        }

        public override void PrepareReuse()
        {
        }
    }
}