using System.Linq;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallEffectManagers.Contracts;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.ServiceCollections;
using Sirenix.Utilities;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallEffectManagers
{
    public class BallEffectManager : ServiceCollection<BallEffect>, IReusable, IInitializing
    {
        [SerializeField] private Transform container;
        [SerializeField] private BallEffect[] effects;

        public void Initialize()
        {
            foreach (var effect in effects)
            {
                AddService(effect);
            }
        }
        
        public void PrepareReuse()
        {
            Clear();
        }

        public override void AddService(BallEffect service)
        {
            var instance = Instantiate(service, container);
            base.AddService(instance);
        }

        public override void RemoveService(BallEffect service)
        {
            var instances = Services.Where(s => s.GetType() == service.GetType());
            foreach (var instance in instances)
            {
                base.RemoveService(instance);
                instance.DestroyAction();
            }
        }

        protected override void Clear()
        {
            Services.ForEach(s => s.DestroyAction());
            base.Clear();
        }
    }
}