using Application.Scripts.Library.GameActionManagers.Contracts;

namespace Application.Scripts.Library.GameActionManagers.Timer
{
    public class ActionTimer
    {
        private IGameAction _gameAction;
        private IActionTimeScale _timeScale;

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
            
            _gameAction?.OnBegin(Time);
            
            return ActionHandler = new ActionHandler(this);
        }

        public void Update(float time)
        {
            if (!Active) return;

            if (_timeScale != null)
            {
                time *= _timeScale.TimeScale;
            }

            Time -= time;
            
            _gameAction?.OnUpdate(time);

            if (Time <= 0f)
            {
                Complete();
            }
        }

        public void Stop()
        {
            _gameAction = null;
            Active = false;
        }

        public void Complete()
        {
            _gameAction?.OnComplete();
            Stop();
        }
    }
}