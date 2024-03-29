namespace Application.Scripts.Library.GameActionManagers.Timer
{
    public struct ActionInfo
    {
        public float SecondsLeft { get; private set; }
        public float DeltaTime { get; private set; }
        public float UnscaledDeltaTime { get; private set; }
        public ActionHandler ActionHandler { get; private set; }

        public ActionInfo(float secondsLeft, float deltaTime, float unscaledDeltaTime, ActionHandler actionHandler)
        {
            SecondsLeft = secondsLeft;
            DeltaTime = deltaTime;
            UnscaledDeltaTime = unscaledDeltaTime;
            ActionHandler = actionHandler;
        }
    }
}