using Application.Scripts.Library.GameActionManagers.Contracts;

namespace Application.Scripts.Library.GameActionManagers.Timer
{
    public readonly struct ActionHandler
    {
        private readonly ActionTimer _actionTimer;
        private readonly IGameAction _gameAction;

        public bool Valid => ValidCheck();
        
        public ActionHandler(ActionTimer actionTimer)
        {
            _actionTimer = actionTimer;
            _gameAction = actionTimer.GameAction;
        }

        public void ChangeTime(float time)
        {
            if (Valid)
            {
                _actionTimer.Time = time;
            }
        }

        public void Complete()
        {
            if (Valid)
            {
                _actionTimer.Complete();
            }
        }
        
        public void Stop()
        {
            if (Valid)
            {
                _actionTimer.Stop();
            }
        }

        private bool ValidCheck()
        {
            if (_actionTimer is { GameAction: { } } && _gameAction != null)
            {
                return _actionTimer.GameAction == _gameAction;
            }

            return false;
        }
    }
}