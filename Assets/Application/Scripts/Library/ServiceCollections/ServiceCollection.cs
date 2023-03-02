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

        public void AddService(TService service)
        {
            _services.Add(service);
        }

        public void RemoveService(TService service)
        {
            _services[_services.IndexOf(service)] = null;
        }

        private IEnumerable<TService> GetServices()
        {
            ClearCollection();

            return Get();
            
            IEnumerable<TService> Get()
            {
                int lastIndex = _services.Count - 1;
                for (int i = lastIndex; i >= 0; i++)
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