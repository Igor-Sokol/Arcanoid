using System;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.PopUpManagers.GameActions
{
    public class WinGameAction : IGameAction
    {
        private readonly ITimeScaler _timeScaler;
        private readonly Vector2 _scaleValue;

        private float _startTimeLeft;

        public event Action OnCompleteAction;
        
        public WinGameAction(ITimeScaler timeScaler, Vector2 scaleValue)
        {
            _timeScaler = timeScaler;
            _scaleValue = scaleValue;
        }
        
        public void OnBegin(ActionInfo actionInfo)
        {
            _startTimeLeft = actionInfo.SecondsLeft;
        }

        public void OnUpdate(ActionInfo actionInfo)
        {
            if (actionInfo.DeltaTime > 0f)
            {
                _timeScaler.Scale = Mathf.Lerp(_scaleValue.y, _scaleValue.x, actionInfo.SecondsLeft / _startTimeLeft);
            }
        }

        public void OnComplete(ActionInfo actionInfo)
        {
            OnCompleteAction?.Invoke();
            OnCompleteAction = null;
        }

        public void OnStop(ActionInfo actionInfo)
        {
        }
    }
}