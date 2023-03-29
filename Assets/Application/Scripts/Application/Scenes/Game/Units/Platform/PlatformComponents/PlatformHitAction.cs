using Application.Scripts.Application.Scenes.Shared.Effects;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Platform.PlatformComponents
{
    public class PlatformHitAction : MonoBehaviour
    {
        [SerializeField] private Puncher puncher;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            puncher.Punch();
        }
    }
}