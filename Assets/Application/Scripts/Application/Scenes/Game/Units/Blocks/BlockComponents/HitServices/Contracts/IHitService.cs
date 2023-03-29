using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Contracts
{
    public interface IHitService : IReusable
    {
        void OnHitAction(Collision2D col);
    }
}