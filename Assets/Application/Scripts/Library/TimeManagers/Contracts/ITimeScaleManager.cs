namespace Application.Scripts.Library.TimeManagers.Contracts
{
    public interface ITimeScaleManager
    {
        public T GetTimeScale<T>() where T : TimeScaler;
    }
}