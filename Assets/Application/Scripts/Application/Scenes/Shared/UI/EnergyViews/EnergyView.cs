using Application.Scripts.Application.Scenes.Shared.UI.StringViews;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Shared.UI.EnergyViews
{
    public class EnergyView : MonoBehaviour
    {
        private Sequence _progressTween;
        private Tween _timeTween;
        private bool _timeEnabled;
        
        [SerializeField] private Slider slider;
        [SerializeField] private ProgressView progressView;
        [SerializeField] private TimeView timeView;
        [SerializeField] private RectTransform timeContainer;
        [SerializeField] private float duration;
        [SerializeField] private float showDuration;

        public Tween SetProgress(int currentEnergy, int maxEnergy)
        {
            _progressTween?.Kill();
            _progressTween = DOTween.Sequence();
            
            _progressTween.Join(slider.DOValue(currentEnergy / (float)maxEnergy, duration));
            _progressTween.Join(progressView.SetProgress(currentEnergy, maxEnergy));

            return _progressTween;
        }
        
        public void SetProgressImmediately(int currentEnergy, int maxEnergy)
        {
            _progressTween?.Kill();
            
            slider.value = currentEnergy / (float)maxEnergy;
            progressView.SetProgressImmediately(currentEnergy, maxEnergy);
        }

        public void SetTimeLeft(float time)
        {
            if (time > 0 && !_timeEnabled)
            {
                ShowTime();
            }
            else if (time <= 0 && _timeEnabled)
            {
                HideTime();
            }
            
            
            timeView.SetTime(Mathf.CeilToInt(time));
        }

        private void ShowTime()
        {
            _timeEnabled = true;
            _timeTween?.Kill();
            
            _timeTween = DOTween.To(() => timeContainer.anchoredPosition,
                (value) => timeContainer.anchoredPosition = value, Vector2.zero, showDuration);
        }

        private void HideTime()
        {
            _timeEnabled = false;
            _timeTween?.Kill();
            
            _timeTween = DOTween.To(() => timeContainer.anchoredPosition,
                (value) => timeContainer.anchoredPosition = value, new Vector2(0, timeContainer.rect.size.y),
                showDuration);
        }

        private void OnEnable()
        {
            timeContainer.anchoredPosition = new Vector2(0, timeContainer.rect.size.y);
            _timeEnabled = false;
        }

        private void OnDisable()
        {
            _progressTween?.Kill();
            _timeTween?.Kill();
        }
    }
}