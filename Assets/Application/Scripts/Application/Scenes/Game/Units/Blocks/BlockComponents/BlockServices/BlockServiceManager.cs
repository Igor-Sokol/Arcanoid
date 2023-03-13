using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices.Contracts;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.ServiceCollections;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices
{
    public class BlockServiceManager : ServiceCollection<IBlockService>, IReusable
    {
        [SerializeField] private BlockService[] blockServices;

        public void PrepareReuse()
        {
            Clear();

            foreach (var service in blockServices)
            {
                service.PrepareReuse();
                AddService(service);
            }
        }
    }
}