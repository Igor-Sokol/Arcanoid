using DG.Tweening;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Screen.PopUps.WinGamePopUps.Animators.PackAnimators
{
    public class PackAnimator : MonoBehaviour
    {
        private Sequence _animation;
        
        [SerializeField] private Transform packContainer;
        [SerializeField] private PackAnimatorConfig animatorConfig;

        public void Enable()
        {
            _animation?.Kill();
            _animation = DOTween.Sequence();

            packContainer.localScale = Vector3.one;
            _animation.Append(packContainer.DOScale(animatorConfig.Scale, animatorConfig.ScaleTime).SetEase(Ease.InOutSine));
            _animation.Append(packContainer.DOScale(Vector3.one, animatorConfig.ScaleTime).SetEase(Ease.InOutSine));
            _animation.SetLoops(-1);
        }

        public void Disable()
        {
            _animation?.Kill();
        }

        private void OnEnable() => Enable();
        private void OnDisable() => Disable();
    }
}