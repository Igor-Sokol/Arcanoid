using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.InitializeManager.Contracts;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts
{
    public interface IBoost : IInitializing
    {
        float Duration { get; }
        void Configure(Block block);
        void RegisterHandler(ActionHandler handler);
        void Enable();
        void Disable();
    }
}