using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PlayingField.Effectors
{
    public class Effector : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private Vector2 positionPercentage;

        public Vector2 PositionPercentage => positionPercentage;

        public void Play()
        {
            particle.Play();
        }

        public void Stop()
        {
            particle.Stop();
        }
    }
}