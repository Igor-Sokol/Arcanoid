using System;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Contracts;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.ServiceCollections;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices
{
    public class DestroyServiceManager : ServiceCollection<IDestroyService>, IReusable, IInitializing
    {
        private bool _destroyed;
        
        [SerializeField] private Block block;
        [SerializeField] private DestroyService[] destroyServices;

        public void Initialize()
        {
            foreach (var service in destroyServices)
            {
                service.Initialize();
            }
        }
        
        public void Destroy()
        {
            if (!_destroyed)
            {
                foreach (var service in destroyServices)
                {
                    service.OnDestroyAction(block);
                }
            }
            
            _destroyed = true;
        }
        
        public void PrepareReuse()
        {
            _destroyed = false;
            Clear();

            foreach (var service in destroyServices)
            {
                service.PrepareReuse();
                AddService(service);
            }
        }
    }
}