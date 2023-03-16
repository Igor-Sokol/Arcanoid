using DG.Tweening;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps.Animators
{
    public class RayAnimator : MonoBehaviour
    {
        private Sequence _animation;
        
        [SerializeField] private RectTransform leftSpinRay;
        [SerializeField] private RectTransform rightSpinRay;
        [SerializeField] private Vector3 rotateAngles;
        [SerializeField] private float rotateTime;

        private void OnEnable()
        {
            _animation = DOTween.Sequence();

            _animation.Append(leftSpinRay.DORotate(rotateAngles, rotateTime, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
            _animation.Join(rightSpinRay.DORotate(-rotateAngles, rotateTime, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
            _animation.SetLoops(-1);
        }

        private void OnDisable()
        {
            _animation?.Kill();
        }
    }
}