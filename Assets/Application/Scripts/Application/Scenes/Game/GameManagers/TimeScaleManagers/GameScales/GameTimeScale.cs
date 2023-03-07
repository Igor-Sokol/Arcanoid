using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers.GameScales
{
    public class GameTimeScale : TimeScaler, IInitializing
    {
        [SerializeField] private float startScale;
        public override float Scale { get; set; }
        public void Initialize()
        {
            Scale = startScale;
        }
    }
}