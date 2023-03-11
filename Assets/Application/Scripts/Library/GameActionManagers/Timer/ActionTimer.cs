using Application.Scripts.Library.GameActionManagers.Contracts;

namespace Application.Scripts.Library.GameActionManagers.Timer
{
    public class ActionTimer
    {
        private IGameAction _gameAction;
        private IActionTimeScale _timeScale;
        private bool _looped;

        public bool Active { get; private set; }
        public ActionHandler ActionHandler { get; private set; }
        public IGameAction GameAction => _gameAction;
        public float Time { get; set; }

        public ActionHandler Start(IGameAction gameAction, float time, IActionTimeScale timeScale = null)
        {
            Active = true;
            _gameAction = gameAction;
            _timeScale = timeScale;
            Time = time;
            _looped = time < 0;

            _gameAction?.OnBegin(new ActionInfo(time, 0f, 0f));
            
            return ActionHandler = new ActionHandler(this);
        }

        public void Update(float time)
        {
            if (!Active) return;

            float scaledTime = time;
            if (_timeScale != null)
            {
                scaledTime *= _timeScale.TimeScale;
            }

            Time -= _looped ? 0 : time;
            
            _gameAction?.OnUpdate(new ActionInfo(Time, scaledTime, time));

            if (Time <= 0f && !_looped && Active)
            {
                Complete();
            }
        }

        public void Stop()
        {
            _gameAction.OnStop(new ActionInfo(Time, 0f, 0f));
            _gameAction = null;
            ActionHandler = default;
            Active = false;
            _looped = false;
        }

        public void Complete()
        {
            var gameAction = _gameAction;
            Stop();
            gameAction.OnComplete(new ActionInfo(Time, 0f, 0f));
        }
    }
}