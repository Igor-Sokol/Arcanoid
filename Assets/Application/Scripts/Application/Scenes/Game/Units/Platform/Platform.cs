using Application.Scripts.Application.Scenes.Game.Units.Platform.PlatformComponents;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.TimeManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Platform
{
    public class Platform : MonoBehaviour, IInitializing, IReusable
    {
        [SerializeField] private PlatformMover platformMover;
        [SerializeField] private PlatformSize platformSize;
        [SerializeField] private PlatformView platformView;
        [SerializeField] private BallLauncher ballLauncher;
        [SerializeField] private TimeManager timeManager;

        public PlatformMover PlatformMover => platformMover;
        public PlatformSize PlatformSize => platformSize;
        public PlatformView PlatformView => platformView;
        public BallLauncher BallLauncher => ballLauncher;
        public TimeManager TimeManager => timeManager;
        
        public void Initialize()
        {
            platformView.Initialize();
            platformSize.Initialize();
            platformMover.Initialize();
        }

        public void PrepareReuse()
        {
            PlatformMover.PrepareReuse();
        }
    }
}
