using System.Collections.Generic;
using UnityEngine;

namespace Application.Scripts.Library.ServiceCollections
{
    public class ServiceCollection<TService> : MonoBehaviour 
        where TService : class
    {
        private readonly List<TService> _services = new List<TService>();

        public IEnumerable<TService> Services => GetServices();

        protected void Clear()
        {
            _services.Clear();
        }

        public virtual void AddService(TService service)
        {
            _services.Add(service);
        }

        public virtual void RemoveService(TService service)
        {
            int index = _services.IndexOf(service);
            if (index >= 0)
            {
                _services[_services.IndexOf(service)] = null;
            }
        }

        private IEnumerable<TService> GetServices()
        {
            ClearCollection();

            return Get();
            
            IEnumerable<TService> Get()
            {
                int count = _services.Count;
                for (int i = 0; i < count; i++)
                {
                    TService service = _services[i];
                    if (service != null)
                    {
                        yield return service;
                    }
                }
            }
        }

        private void ClearCollection()
        {
            _services.RemoveAll(s => s is null);
        }
    }
}