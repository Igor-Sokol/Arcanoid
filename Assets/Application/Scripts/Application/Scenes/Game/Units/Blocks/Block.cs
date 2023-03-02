using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents;
using UnityEngine;
using UnityEngine.Serialization;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private BlockView blockView;

        public BlockView BlockView => blockView;
    }
}