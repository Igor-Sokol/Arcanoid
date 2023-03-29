using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Contracts;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.ServiceCollections;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices
{
    public class HitServiceManager : ServiceCollection<IHitService>, IReusable
    {
        [SerializeField] private HitService[] hitServices;

        public void PrepareReuse()
        {
            Clear();

            foreach (var service in hitServices)
            {
                service.PrepareReuse();
                AddService(service);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            foreach (var service in Services)
            {
                service.OnHitAction(col);
            }
        }
    }
}