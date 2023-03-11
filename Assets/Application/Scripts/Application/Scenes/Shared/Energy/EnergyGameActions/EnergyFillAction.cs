using System;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;

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
            _timeLeftAction = timeLeftAction;
        }

        public void OnBegin(ActionInfo actionInfo)
        {
            _timeLeftAction?.Invoke(actionInfo.SecondsLeft);
        }

        public void OnUpdate(ActionInfo actionInfo)
        {
            _timeLeftAction?.Invoke(actionInfo.SecondsLeft);
        }

        public void OnComplete(ActionInfo actionInfo)
        {
            _energyManager.AddEnergy(1);
            _startFillAction?.Invoke();
        }

        public void OnStop(ActionInfo actionInfo)
        {
        }
    }
}