using Application.Scripts.Library.InitializeManager.Contracts;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts
{
    public interface IBoost : IInitializing
    {
        float Duration { get; }
        void Enable();
        void Disable();
    }
}