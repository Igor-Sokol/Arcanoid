using System;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers
{
    public class HealthManager : MonoBehaviour, IHealthManager, IInitializing, IReusable
    {
        private int _currentHealth;
        
        [SerializeField] private int maxHealth;
        [SerializeField] private int startHealth;

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;

        public event Action OnHealthAdded;
        public event Action OnHealthRemoved;
        public event Action OnDead;
        public event Action OnPrepareReuse;

        public void Initialize()
        {
            _currentHealth = Mathf.Clamp(startHealth, 0, maxHealth);
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
            _currentHealth--;
            OnHealthRemoved?.Invoke();

            if (_currentHealth <= 0)
            {
                OnDead?.Invoke();
            }
        }
    }
}