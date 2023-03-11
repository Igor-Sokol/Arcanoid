using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;
using DG.Tweening;

namespace Application.Scripts.Application.Scenes.Shared.DoTweenGameActions
{
    public class DoTweenGameAction : IGameAction
    {
        private readonly Tween _tween;

        public DoTweenGameAction(Tween tween)
        {
            _tween = tween;
            tween.SetUpdate(UpdateType.Manual);
            _tween.SetAutoKill(false);
        }

        public void OnBegin(ActionInfo actionInfo)
        {
        }

        public void OnUpdate(ActionInfo actionInfo)
        {
            if (_tween.active)
            {
                _tween.ManualUpdate(actionInfo.DeltaTime, actionInfo.UnscaledDeltaTime);
            }
        }

        public void OnComplete(ActionInfo actionInfo)
        {
            if (_tween.active)
            {
                _tween.Complete();
            }
        }

        public void OnStop(ActionInfo actionInfo)
        {
            if (_tween.active)
            {
                _tween.Kill();
            }
        }
    }
}