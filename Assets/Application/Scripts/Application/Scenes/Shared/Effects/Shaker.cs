using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.Effects
{
    public class Shaker : MonoBehaviour
    {
        private Tween _animation;

        [SerializeField] private Transform shakeTransform;
        [SerializeField] private float duration;
        [SerializeField] private float  strength = 1f;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float randomness = 90f;
        [SerializeField] private bool snapping = false;
        [SerializeField] private bool fadeOut = true;
    
        [Button("Shake")]
        public void Shake()
        {
            _animation?.Complete();
            _animation = shakeTransform.DOShakePosition(duration, strength, vibrato, randomness, snapping, fadeOut);
        }

        public void Stop()
        {
            _animation?.Complete();
        }

        private void OnDisable()
        {
            _animation?.Complete();
        }
    }
}
