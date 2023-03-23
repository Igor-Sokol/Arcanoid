using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Game.Screen.UI.Background
{
    public class LineAnimator : MonoBehaviour
    {
        private Sequence _animation;
    
        [SerializeField] private Image image;
        [SerializeField] private float stepDuration;
        private void Play()
        {
            _animation?.Kill();
            _animation = DOTween.Sequence();

            image.fillAmount = 0;
            _animation.Append(image.DOFillAmount(1, stepDuration).SetEase(Ease.Linear));
            _animation.AppendCallback(() => image.fillOrigin = 0);
            _animation.Append(image.DOFillAmount(0, stepDuration).SetEase(Ease.Linear));
            _animation.AppendCallback(() => image.fillOrigin = 1);
            _animation.SetLoops(-1);
        }

        private void Stop()
        {
            _animation?.Kill();
        }

        private void OnEnable() => Play();

        private void OnDisable() => Stop();
    }
}
