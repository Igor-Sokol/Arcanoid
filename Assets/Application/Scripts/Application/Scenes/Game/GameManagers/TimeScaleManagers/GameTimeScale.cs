using Application.Scripts.Library.GameActionManagers.Contracts;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers
{
    public class GameTimeScale : TimeScaler, IInitializing, IActionTimeScale
    {
        [SerializeField] private float startScale;
        
        public override float Scale { get; set; }
        public float TimeScale => Scale;
        
        public void Initialize()
        {
            Scale = startScale;
        }
    }
}