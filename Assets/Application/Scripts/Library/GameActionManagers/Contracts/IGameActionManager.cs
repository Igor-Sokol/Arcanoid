using System.Collections.Generic;
using Application.Scripts.Library.GameActionManagers.Timer;

namespace Application.Scripts.Library.GameActionManagers.Contracts
{
    public interface IGameActionManager
    {
        ActionHandler StartAction(IGameAction gameAction, float time, IActionTimeScale timeScale = null);
        IEnumerable<ActionHandler> GetGameAction<T>() where T : IGameAction;
    }
}