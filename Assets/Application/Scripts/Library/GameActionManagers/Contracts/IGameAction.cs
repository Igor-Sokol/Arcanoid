namespace Application.Scripts.Library.GameActionManagers.Contracts
{
    public interface IGameAction
    {
        void OnBegin(float secondsLeft);
        void OnUpdate(float secondsLeft);
        void OnComplete();
    }
}