using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.TimeManagers;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.TimeManagers
{
    public class ActionTimeManagerMono : TimeManagerMono, IActionTimeScale
    {
        public float TimeScale => base.Scale;
    }
}