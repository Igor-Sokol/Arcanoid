using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Implementations
{
    public class HitParticleService : HitService
    {
        [SerializeField] private ParticleSystem particle;
        
        public override void OnHitAction(Collision2D col)
        {
            var parent = transform;
            var instance = Instantiate(particle, col.GetContact(0).point, Quaternion.identity, parent);
            instance.Play();
        }

        public override void PrepareReuse()
        {
        }
    }
}