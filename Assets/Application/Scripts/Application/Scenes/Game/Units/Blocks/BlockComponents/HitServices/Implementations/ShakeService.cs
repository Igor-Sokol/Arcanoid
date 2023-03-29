using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Contracts;
using Application.Scripts.Application.Scenes.Shared.Effects;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Implementations
{
    public class ShakeService : HitService
    {
        [SerializeField] private Shaker shaker;
        
        public override void OnHitAction(Collision2D col)
        {
            OnHitAction();
        }
        
        public void OnHitAction()
        {
            shaker.Shake();
        }

        public override void PrepareReuse()
        {
            shaker.Stop();
        }
    }
}