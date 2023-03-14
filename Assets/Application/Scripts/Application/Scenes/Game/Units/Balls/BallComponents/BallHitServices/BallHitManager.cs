using System;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Contracts;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.ServiceCollections;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices
{
    public class BallHitManager : ServiceCollection<IBallHitService>, IReusable
    {
        [SerializeField] private Ball ball;
        
        public void PrepareReuse()
        {
            Clear();
        }

        public override void AddService(IBallHitService service)
        {
            base.AddService(service);
            service.Ball = ball;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            foreach (var service in Services)
            {
                service.OnCollisionAction(col);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            foreach (var service in Services)
            {
                service.OnTriggerAction(col);
            }
        }
    }
}