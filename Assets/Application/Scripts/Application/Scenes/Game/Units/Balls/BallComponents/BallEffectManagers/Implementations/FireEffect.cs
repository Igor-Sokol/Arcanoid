using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallEffectManagers.Contracts;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallEffectManagers.Implementations
{
    public class FireEffect : BallEffect
    {
        public override void DestroyAction()
        {
            Destroy(gameObject);
        }
    }
}