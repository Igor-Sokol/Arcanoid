using System;
using Application.Scripts.Library.GameActionManagers.Contracts;

namespace Application.Scripts.Application.Scenes.Shared.Energy.EnergyGameActions
{
    public class EnergyFillAction : IGameAction
    {
        private readonly EnergyManager _energyManager;
        private readonly Action _startFillAction;
        private readonly Action<float> _timeLeftAction;
        
        public EnergyFillAction(EnergyManager energyManager, Action startFillAction, Action<float> timeLeftAction)
        {
            _energyManager = energyManager;
            _startFillAction = startFillAction;
        }
        
        public void OnBegin(float secondsLeft)
        {
            _timeLeftAction?.Invoke(secondsLeft);
        }

        public void OnUpdate(float secondsLeft)
        {
            _timeLeftAction?.Invoke(secondsLeft);
        }

        public void OnComplete()
        {
            _energyManager.AddEnergy(1);
            _startFillAction?.Invoke();
        }

        public void OnStop()
        {
        }
    }
}