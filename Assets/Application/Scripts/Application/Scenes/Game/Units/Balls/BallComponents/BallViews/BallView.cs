using Application.Scripts.Library.Reusable;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.BallViews
{
    public class BallView : MonoBehaviour, IReusable
    {
        [SerializeField] private SpriteRenderer sprite;

        public Color SpriteColor => sprite.color;

        public void PrepareReuse()
        {
            sprite.color = Color.white;
        }
    }
}