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
            foreach (var service in destroyServices)
            {
                service.OnDestroyAction(block);
            }
        }
        
        public void PrepareReuse()
        {
            Clear();

            foreach (var service in destroyServices)
            {
                service.PrepareReuse();
                AddService(service);
            }
        }
    }
}