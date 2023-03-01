using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private BlockRenderer blockRenderer;

        public BlockRenderer BlockRenderer => blockRenderer;
    }
}