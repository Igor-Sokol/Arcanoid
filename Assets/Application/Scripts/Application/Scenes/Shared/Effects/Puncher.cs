using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.Effects
{
    public class Puncher : MonoBehaviour
    {
        private Tween _animation;

        [SerializeField] private Transform punchTransform;
        [SerializeField] private Vector3 punch;
        [SerializeField] private float duration;
        [SerializeField] private int vibrato = 10;
        [SerializeField] private float elasticity = 1F;
        [SerializeField] private bool snapping = false;
    
        [Button("Punch")]
        public void Punch()
        {
            _animation?.Complete();
            _animation = punchTransform.DOPunchPosition(punch, duration, vibrato, elasticity, snapping);
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