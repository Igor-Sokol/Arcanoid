using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Reusable;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Contracts
{
    public interface IDestroyService : IReusable, IInitializing
    {
        void OnDestroyAction(Block block);
    }
}