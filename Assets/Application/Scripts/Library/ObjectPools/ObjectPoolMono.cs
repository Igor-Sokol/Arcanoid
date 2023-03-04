using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Application.Scripts.Library.ObjectPools
{
    public class ObjectPoolMono<T> : ObjectPool<T> 
        where T : MonoBehaviour
    {
        private readonly Transform _container;
        
        public ObjectPoolMono(Func<T> factory, Transform container) : base(factory)
        {
            _container = container;
        }

        protected override void GetAction(T item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void ReturnAction(T item)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(_container);
        }

        protected override void RemoveAction(T item)
        {
            Object.Destroy(item);
        }
    }
}