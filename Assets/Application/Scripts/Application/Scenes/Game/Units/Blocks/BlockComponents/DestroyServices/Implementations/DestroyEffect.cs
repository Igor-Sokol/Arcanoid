using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Implementations
{
    public class DestroyEffect : DestroyService
    {
        [SerializeField] private ParticleSystem particle;
        
        public override void Initialize()
        {
        }

        public override void PrepareReuse()
        {
        }

        public override void OnDestroyAction(Block block)
        {
            var instance = Instantiate(particle, transform.position, Quaternion.identity);
            instance.Play();
        }
    }
}