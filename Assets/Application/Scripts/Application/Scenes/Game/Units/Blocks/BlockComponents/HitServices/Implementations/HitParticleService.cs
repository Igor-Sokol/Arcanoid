using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Implementations
{
    public class HitParticleService : HitService
    {
        [SerializeField] private ParticleSystem particle;
        
        public override void OnHitAction(Collision2D col)
        {
            OnHitAction(col.GetContact(0).point);
        }

        public void OnHitAction(Vector3 position)
        {
            var instance = Instantiate(particle, position, Quaternion.identity);
            instance.Play();
        }

        public override void PrepareReuse()
        {
        }
    }
}