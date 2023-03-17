using Application.Scripts.Application.Scenes.Game.GameManagers.BallsManagers.BallSetUpServices.Contracts;
using Application.Scripts.Application.Scenes.Game.Units.Balls;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallEffectManagers.Implementations;
using Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallHitServices.Implementations;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Boosts.Implementations.FireBoost.FireBallSetUpActions
{
    public class FireBallSetUpAction : IBallSetUpAction
    {
        private readonly FireBall _fireBall;
        private readonly FireEffect _fireEffect;
        private readonly Color _color;

        private Color _previousColor;
        
        public FireBallSetUpAction(FireEffect fireEffect, Color color)
        {
            _fireBall = new FireBall();
            _fireEffect = fireEffect;
            _color = color;
        }
        
        public void Install(Ball ball)
        {
            ball.BallHitManager.AddService(_fireBall);
            ball.BallEffectManager.AddService(_fireEffect);
            _previousColor = ball.BallView.SpriteColor;
            ball.BallView.SpriteColor = _color;
        }

        public void UnInstall(Ball ball)
        {
            ball.BallHitManager.RemoveService(_fireBall);
            ball.BallEffectManager.RemoveService(_fireEffect);
            ball.BallView.SpriteColor = _previousColor;
        }
    }
}