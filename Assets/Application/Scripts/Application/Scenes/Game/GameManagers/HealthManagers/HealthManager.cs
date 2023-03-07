using System;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.HealthManagers
{
    public class HealthManager : MonoBehaviour, IInitializing
    {
        private int _currentHealth;
        
        [SerializeField] private int maxHealth;
        [SerializeField] private int startHealth;

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;

        public event Action OnHealthAdded;
        public event Action OnHealthRemoved;
        public event Action OnDead;

        public void Initialize()
        {
            _currentHealth = Mathf.Clamp(startHealth, 0, maxHealth);
        }
        
        public void AddHealth()
        {
            _currentHealth++;
            OnHealthAdded?.Invoke();
        }

        public void RemoveHealth()
        {
            _currentHealth--;
            OnHealthRemoved?.Invoke();

            if (_currentHealth <= 0)
            {
                OnDead.Invoke();
            }
        }
    }
}