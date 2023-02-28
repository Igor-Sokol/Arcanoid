namespace Application.Scripts.Library.ObjectPools.Contracts
{
    public interface IObjectPool<T>
    {
        int ItemCount { get; }
        T Get();
        void Return(T item);
        void Resize(int itemCount);
    }
}