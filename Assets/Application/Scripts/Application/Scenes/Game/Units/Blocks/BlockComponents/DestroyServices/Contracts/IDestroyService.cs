using Application.Scripts.Library.Reusable;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Contracts
{
    public interface IDestroyService : IReusable
    {
        void OnDestroyAction();
    }
}