using System;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.HitServices.Implementations
{
    public class BlockHealthService : HitService
    {
        private int _actualHealth;

        [SerializeField] private DestroyServiceManager destroyManager;
        [SerializeField] private int startHealth;

        public event Action<int> OnHealthRemoved;
        public event Action OnDead; 

        public override void OnHitAction()
        {
            if (_actualHealth > 0)
            {
                _actualHealth--;
            
                OnHealthRemoved?.Invoke(_actualHealth);

                if (_actualHealth <= 0)
                {
                    OnDead?.Invoke();
                    destroyManager.Destroy();
                }
            }
        }

        public override void PrepareReuse()
        {
            _actualHealth = startHealth;
        }
    }
}