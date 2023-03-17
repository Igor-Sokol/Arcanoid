using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallEffectManagers.Contracts;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.ServiceCollections;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.BallSharedEffects
{
    public class BallEffectContainer : ServiceCollection<BallEffect>, IReusable
    {
        public void PrepareReuse()
        {
            Clear();
        }
    }
}