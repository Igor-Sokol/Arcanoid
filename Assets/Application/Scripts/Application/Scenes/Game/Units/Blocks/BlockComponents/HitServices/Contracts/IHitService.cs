using Application.Scripts.Library.Reusable;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Contracts
{
    public interface IHitService : IReusable
    {
        void OnHitAction();
    }
}