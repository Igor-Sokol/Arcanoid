namespace Application.Scripts.Library.ObjectPools.Contracts
{
    public interface IObjectRemover<in T>
    {
        void OnRemove(T item);
    }
}