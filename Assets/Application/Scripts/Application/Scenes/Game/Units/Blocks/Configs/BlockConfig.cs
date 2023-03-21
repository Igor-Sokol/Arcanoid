using System;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.Configs
{
    [Serializable]
    public struct BlockConfig
    {
        [SerializeField] private Sprite blockImage;

        public Sprite BlockImage => blockImage;
    }
}