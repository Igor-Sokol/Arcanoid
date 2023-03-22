using System;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps.Animators.PackAnimators
{
    [Serializable]
    public struct PackAnimatorConfig
    {
        [SerializeField] private Vector3 scale;
        [SerializeField] private float scaleTime;
        
        public Vector3 Scale => scale;
        public float ScaleTime => scaleTime;
    }
}