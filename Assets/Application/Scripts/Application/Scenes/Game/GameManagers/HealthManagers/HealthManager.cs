using System;
using Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers.HealthConfig;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers
{
    public class HealthManager : IHealthManager, IInitializable
    {
        private int _currentHealth;
        private IHealthConfig _healthConfig;

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _healthConfig.MaxHealth;

        public event Action OnHealthAdded;
        public event Action OnHealthRemoved;
        public event Action OnDead;
        public event Action OnPrepareReuse;

        [Inject]
        private void Construct(IHealthConfig healthConfig)
        {
            _healthConfig = healthConfig;
        }
        
        public void Initialize()
        {
            _currentHealth = Mathf.Clamp(_healthConfig.StartHealth, 0, _healthConfig.MaxHealth);
        }

        public void PrepareReuse()
        {
            Initialize();
            OnPrepareReuse?.Invoke();
        }
        
        public void AddHealth()
        {
            if (_currentHealth + 1 <= MaxHealth)
            {
                _currentHealth++;
                OnHealthAdded?.Invoke();
            }
        }

        public void RemoveHealth()
        {
            if (_currentHealth > 0)
            {
                _currentHealth--;
                OnHealthRemoved?.Invoke();

                if (_currentHealth <= 0)
                {
                    OnDead?.Invoke();
                }
            }
        }
    }
}