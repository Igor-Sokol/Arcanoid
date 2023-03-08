using System;
using Application.Scripts.Application.Scenes.Shared.Energy.Config;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Application.Scenes.Shared.Energy.EnergyGameActions;
using Application.Scripts.Application.Scenes.Shared.Energy.Repository.Contracts;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.Energy
{
    public class EnergyManager : MonoBehaviour, IEnergyManager, IInitializing
    {
        private IGameActionManager _gameActionManager;
        private ActionHandler _fillActionHandler;
        private int _currentEnergy;
        
        [SerializeField] private EnergyManagerConfig config;

        public int CurrentEnergy => _currentEnergy;
        public int MaxEnergy => config.MaxGenerateEnergy;
        
        public event Action OnEnergyAdded;
        public event Action OnEnergyRemoved;
        public event Action<float> OnFillTimeChanged; 

        public void Initialize()
        {
            _gameActionManager = ProjectContext.Instance.GetService<IGameActionManager>();
            LoadEnergy();
        }
        
        public void AddEnergy(int energy)
        {
            _currentEnergy += energy;
            config.EnergyRepository.Save(new EnergySaveObject(_currentEnergy, DateTime.Now));

            if (_currentEnergy >= config.MaxGenerateEnergy)
            {
                _fillActionHandler.Stop();
            }
            
            OnEnergyAdded?.Invoke();
        }

        public void RemoveEnergy(int energy)
        {
            _currentEnergy -= energy;
            config.EnergyRepository.Save(new EnergySaveObject(_currentEnergy, DateTime.Now));

            if (_currentEnergy < config.MaxGenerateEnergy)
            {
                StartFillAction();
            }
            
            OnEnergyRemoved?.Invoke();
        }

        private void StartFillAction(float timeToFill)
        {
            if (_currentEnergy < config.MaxGenerateEnergy)
            {
                _fillActionHandler.Stop();
                var fillAction = new EnergyFillAction(this, StartFillAction, ChangeFillTime);
                _fillActionHandler = _gameActionManager.StartAction(fillAction, timeToFill);
            }
        }
        
        private void StartFillAction()
        {
            StartFillAction(config.EnergyGenerateTime);
        }

        private void ChangeFillTime(float leftTime)
        {
            OnFillTimeChanged?.Invoke(leftTime);
        }
        
        private void LoadEnergy()
        {
            EnergySaveObject energySave = config.EnergyRepository.Load();

            if (energySave.DateTime != default)
            {
                if (energySave.Energy < config.MaxGenerateEnergy)
                {
                    TimeSpan deltaTime = DateTime.Now - energySave.DateTime;
                    int offlineEnergy = (int)(deltaTime.TotalSeconds / config.EnergyGenerateTime);
                    _currentEnergy = Mathf.Clamp(offlineEnergy + energySave.Energy, 0, config.MaxGenerateEnergy);
                    StartFillAction(config.EnergyGenerateTime - (int)(deltaTime.TotalSeconds % config.EnergyGenerateTime));
                }
                else
                {
                    _currentEnergy = energySave.Energy;
                    StartFillAction();
                }
            }
            else
            {
                _currentEnergy = config.StartEnergy;
                StartFillAction();
            }
        }
    }
}