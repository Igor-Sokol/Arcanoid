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

        public int ItemCount => _items.Count;

        public ObjectPool(Func<T> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        
        public T Get()
        {
            var item = _items.Count > 0 ? _items.Pop() : CreateItem();
            GetAction(item);

            return item;
        }

        public void Return(T item)
        {
            _items.Push(item);
            ReturnAction(item);
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
                RemoveAction(item);
            }
        }
        
        private T CreateItem()
        {
            var item = _factory?.Invoke();
            ReturnAction(item);
            return item;
        }
        
        protected virtual void GetAction(T item)
        {
        }

        protected virtual void ReturnAction(T item)
        {
        }

        protected virtual void RemoveAction(T item)
        {
        }
    }
}