namespace Application.Scripts.Library.ObjectPools.Contracts
{
    public interface IGetAction<in T>
    {
        void OnGetAction(T item);
    }
}