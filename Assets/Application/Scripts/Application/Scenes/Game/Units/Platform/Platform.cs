using Application.Scripts.Application.Scenes.Game.Units.Platform.PlatformComponents;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Platform
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private PlatformMover platformMover;
        [SerializeField] private PlatformSize platformSize;
        [SerializeField] private PlatformView platformView;
    }
}
