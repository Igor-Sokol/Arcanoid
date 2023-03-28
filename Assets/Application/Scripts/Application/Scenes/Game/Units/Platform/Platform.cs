using Application.Scripts.Application.Scenes.Game.Units.Platform.PlatformComponents;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.TimeManagers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Application.Scripts.Application.Scenes.Game.Units.Platform
{
    public class Platform : MonoBehaviour, IInitializing
    {
        [SerializeField] private PlatformMover platformMover;
        [SerializeField] private PlatformSize platformSize;
        [SerializeField] private PlatformView platformView;
        [SerializeField] private BallLauncher ballLauncher;
        [FormerlySerializedAs("timeManager")] [SerializeField] private TimeManagerMono timeManagerMono;

        public PlatformMover PlatformMover => platformMover;
        public PlatformSize PlatformSize => platformSize;
        public PlatformView PlatformView => platformView;
        public BallLauncher BallLauncher => ballLauncher;
        public TimeManagerMono TimeManagerMono => timeManagerMono;
        
        public void Initialize()
        {
            platformSize.Initialize();
            platformMover.Initialize();
        }
    }
}
