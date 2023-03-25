using System;
using System.Collections.Generic;
using Application.Scripts.Library.DependencyInjection.Contracts;
using UnityEngine;

namespace Application.Scripts.Library.DependencyInjection
{
    public class ProjectContext : MonoBehaviour
    {
        private static ProjectContext _instance;
        public static ProjectContext Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<ProjectContext>();

                    if (!_instance)
                    {
                        var contextResource = Resources.Load<ProjectContext>("ProjectContextOld/ProjectContextOld");
                        ProjectContext context;
                        
                        if (contextResource is null)
                        {
                            var contextContainer = new GameObject();
                            context = contextContainer.AddComponent<ProjectContext>();
                        }
                        else
                        {
                            context = Instantiate(contextResource);
                        }
                        
                        context.name = typeof(ProjectContext).ToString() + " (Singleton)";
                        DontDestroyOnLoad(context.gameObject);
                        _instance = context;
                        
                        context.Init();
                    }
                }

                return _instance;
            }
        }

        private Dictionary<Type, object> _services;
        
        [SerializeField] private ServiceInstaller[] serviceInstallers;

        private void Init()
        {
            _services = new Dictionary<Type, object>();
            
            foreach (var serviceInstaller in serviceInstallers)
            {
                serviceInstaller.ProjectContext = this;
                serviceInstaller.InstallService();
            }
        }

        public TService GetService<TService>()
        {
            if (_services.TryGetValue(typeof(TService), out object value))
            {
                return (TService)value;
            }

            return default;
        }

        public void SetService<TService, TImplementation>(TImplementation service)
        {
            if (_services.ContainsKey(typeof(TService)))
            {
                _services[typeof(TService)] = service;
            }
            else
            {
                _services.Add(typeof(TService), service);
            }
        }
    }
}
