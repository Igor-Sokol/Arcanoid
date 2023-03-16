using System.Linq;
using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.Contracts;
using Application.Scripts.Application.Scenes.Game.Pools.BallProviders.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices.Implementation;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Implementations;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Implementations
{
    public class BulletBall : IBallHitService
    {
        private readonly IBallProvider _ballProvider;
        private readonly int _damage;
        
        public BulletBall(IBallProvider ballProvider, int damage)
        {
            _ballProvider = ballProvider;
            _damage = damage;
        }
        
        public void PrepareReuse()
        {
        }
        
        public void OnCollisionAction(Collision2D col, Ball ball)
        {
        }

        public void OnTriggerAction(Collider2D col, Ball ball)
        {
            if (col.TryGetComponent(out Block block) && block.HitServiceManager.Services.OfType<BlockHealthService>().Any())
            {
                var blockHealth = block.BlockServiceManager.Services.OfType<BlockHealth>().FirstOrDefault();
                if (blockHealth)
                {
                    for (int i = 0; i < _damage; i++)
                    {
                        blockHealth.RemoveHealth();
                    }
                }
            }
            
            ball.PrepareReuse();
            _ballProvider.Return(ball);
        }
    }
}