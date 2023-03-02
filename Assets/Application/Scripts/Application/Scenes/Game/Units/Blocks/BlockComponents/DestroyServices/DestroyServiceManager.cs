using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices.Contracts;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.ServiceCollections;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices
{
    public class DestroyServiceManager : ServiceCollection<IDestroyService>, IReusable
    {
        [SerializeField] private DestroyService[] destroyServices;

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