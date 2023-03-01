using Application.Scripts.Application.Scenes.Game.Pools.BlockProvider.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using UnityEngine;

namespace Application.Scripts.Temp
{
    public class TempBlockProvider : BlockProvider
    {
        [SerializeField] private Block red;
        [SerializeField] private Block blue;
        [SerializeField] private Block iron;
        
        public override Block GetBlock(string key)
        {
            switch (key)
            {
                case nameof(red):
                    return Instantiate(red);
                case nameof(blue):
                    return Instantiate(blue);
                case nameof(iron):
                    return Instantiate(iron);
            }

            return null;
        }
    }
}