namespace Application.Scripts.Library.ObjectPools.Contracts
{
    public interface IReturnAction<in T>
    {
        void OnReturnAction(T item);
    }
}