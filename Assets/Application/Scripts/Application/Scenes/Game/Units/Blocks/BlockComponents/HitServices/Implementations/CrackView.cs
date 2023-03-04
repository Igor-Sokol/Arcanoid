using System.Linq;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Implementations
{
    public class CrackView : HitService
    {
        [SerializeField] private SpriteRenderer[] breakSprites;

        public override void OnHitAction()
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