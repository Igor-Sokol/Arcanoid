using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts
{
    public abstract class Boost : MonoBehaviour, IBoost
    {
        protected Block Block;
        public abstract float Duration { get; }
        public abstract void Initialize();
        public virtual void Configure(Block block) => Block = block;
        public abstract void Enable();
        public abstract void Disable();
    }
}