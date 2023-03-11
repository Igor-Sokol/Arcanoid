using Application.Scripts.Library.TimeManagers.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.GameManagers.TimeScaleManagers
{
    public class GameTimeScale : TimeScaler
    {
        [SerializeField] private float scale;
        
        public override float Scale { get => scale; set => scale = value; }
    }
}