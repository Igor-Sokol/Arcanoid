using System.Linq;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices.Implementation;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices
{
    public class CrackView : BlockService
    {
        [SerializeField] private SpriteRenderer[] breakSprites;
        [SerializeField] private BlockHealth blockHealth;

        private void OnEnable()
        {
            blockHealth.OnHealthRemoved += OnHealthRemove;
        }

        private void OnDisable()
        {
            blockHealth.OnHealthRemoved -= OnHealthRemove;
        }

        private void OnHealthRemove(int health)
        {
            breakSprites.FirstOrDefault(s => !s.gameObject.activeInHierarchy)?.gameObject.SetActive(true);
        }

        public override void PrepareReuse()
        {
            foreach (var sprite in breakSprites)
            {
                sprite.gameObject.SetActive(false);
            }
        }
    }
}