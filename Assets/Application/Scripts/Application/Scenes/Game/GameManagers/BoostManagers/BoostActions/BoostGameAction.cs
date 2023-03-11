using Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.GameActionManagers.Timer;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BoostManagers.BoostActions
{
    public class BoostGameAction : IGameAction
    {
        private readonly IBoost _boost;
        
        public BoostGameAction(IBoost boost)
        {
            _boost = boost;
        }
        
        public void OnBegin(ActionInfo actionInfo)
        {
            _boost.Enable();
        }

        public void OnUpdate(ActionInfo actionInfo)
        {
        }

        public void OnComplete(ActionInfo actionInfo)
        {
        }

        public void OnStop(ActionInfo actionInfo)
        {
            _boost.Disable();
        }
    }
}