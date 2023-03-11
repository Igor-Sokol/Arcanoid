using Application.Scripts.Library.GameActionManagers.Timer;

namespace Application.Scripts.Library.GameActionManagers.Contracts
{
    public interface IGameAction
    {
        void OnBegin(ActionInfo actionInfo);
        void OnUpdate(ActionInfo actionInfo);
        void OnComplete(ActionInfo actionInfo);
        void OnStop(ActionInfo actionInfo);
    }
}