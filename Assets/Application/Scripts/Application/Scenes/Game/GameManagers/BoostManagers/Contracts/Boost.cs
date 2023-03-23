using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Library.GameActionManagers.Timer;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts
{
    public abstract class Boost : MonoBehaviour, IBoost
    {
        protected Block Block;
        public abstract float Duration { get; }
        public abstract void Initialize();
        public virtual void Configure(Block block) => Block = block;
        public virtual void RegisterHandler(ActionHandler handler) { }
        public virtual void Enable() {}
        public virtual void Disable() {}
    }
}