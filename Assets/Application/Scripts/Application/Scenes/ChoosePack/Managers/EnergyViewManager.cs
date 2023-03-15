using System;
using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Application.Scenes.Shared.UI.EnergyViews;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.ChoosePack.Managers
{
    public class EnergyViewManager : MonoBehaviour, IInitializing
    {
        private IEnergyManager _energyManager;
        
        [SerializeField] private EnergyView energyView;

        public void Initialize()
        {
            _energyManager = ProjectContext.Instance.GetService<IEnergyManager>();
            
            _energyManager.OnEnergyAdded += EnergyUpdate;
            _energyManager.OnEnergyRemoved += EnergyUpdate;
            _energyManager.OnFillTimeChanged += EnergyTimeUpdate;
            
            EnergyUpdate();
        }
        
        private void OnEnable()
        {
            if (_energyManager != null)
            {
                _energyManager.OnEnergyAdded += EnergyUpdate;
                _energyManager.OnEnergyRemoved += EnergyUpdate;
                _energyManager.OnFillTimeChanged += EnergyTimeUpdate;
            }
        }

        private void OnDisable()
        {
            if (_energyManager != null)
            {
                _energyManager.OnEnergyAdded -= EnergyUpdate;
                _energyManager.OnEnergyRemoved -= EnergyUpdate;
                _energyManager.OnFillTimeChanged -= EnergyTimeUpdate;
            }
        }

        private void EnergyUpdate()
        {
            energyView.SetProgress(_energyManager.CurrentEnergy, _energyManager.MaxEnergy);
        }

        private void EnergyTimeUpdate(float time)
        {
            energyView.SetTimeLeft(time);
        }
    }
}