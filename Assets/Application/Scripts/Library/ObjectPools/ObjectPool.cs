using System;
using System.Collections.Generic;
using Application.Scripts.Library.ObjectPools.Contracts;

namespace Application.Scripts.Library.ObjectPools
{
    public class ObjectPool<T> : IObjectPool<T> 
        where T : class
    {
        private readonly Stack<T> _items = new Stack<T>();
        private readonly Func<T> _factory;
        private readonly IObjectRemover<T> _objectRemover;
        private readonly IGetAction<T> _getAction;
        private readonly IReturnAction<T> _returnAction;

        public int ItemCount => _items.Count;

        public ObjectPool(Func<T> factory, IObjectRemover<T> objectRemover, 
            IGetAction<T> getAction = null, IReturnAction<T> returnAction = null)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _objectRemover = objectRemover ?? throw new ArgumentNullException(nameof(objectRemover));
            _getAction = getAction;
            _returnAction = returnAction;
        }
        
        public T Get()
        {
            var item = _items.Count > 0 ? _items.Pop() : CreateItem();
            _getAction?.OnGetAction(item);

            return item;
        }

        public void Return(T item)
        {
            _items.Push(item);
            _returnAction.OnReturnAction(item);
        }

        public void Resize(int itemCount)
        {
            var countDifference = itemCount - ItemCount;
            AddElements(countDifference);
            RemoveElements(-countDifference);
        }

        private void AddElements(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _items.Push(CreateItem());
            }
        }

        private void RemoveElements(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var item = _items.Pop();
                _objectRemover.OnRemove(item);
            }
        }
        
        private T CreateItem()
        {
            return _factory?.Invoke();
        }
    }
}