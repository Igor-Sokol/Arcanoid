using System;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.DestroyServices;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Blocks.BlockComponents.BlockServices.Implementation
{
    public class BlockHealth : BlockService
    {
        private int _actualHealth;

        [SerializeField] private DestroyServiceManager destroyManager;
        [SerializeField] private int startHealth;

        public event Action<int> OnHealthRemoved;
        public event Action OnDead;

        public void RemoveHealth()
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