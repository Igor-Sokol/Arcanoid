using Application.Scripts.Application.Scenes.Shared.UI.StringViews;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Shared.UI.EnergyViews
{
    public class EnergyView : MonoBehaviour
    {
        private Sequence _tween;
        
        [SerializeField] private Slider slider;
        [SerializeField] private ProgressView progressView;
        [SerializeField] private TimeView timeView;
        [SerializeField] private float duration;

        public Tween SetProgress(int currentEnergy, int maxEnergy)
        {
            _tween?.Kill();
            _tween = DOTween.Sequence();
            
            _tween.Join(slider.DOValue(currentEnergy / (float)maxEnergy, duration));
            _tween.Join(progressView.SetProgress(currentEnergy, maxEnergy));

            return _tween;
        }
        
        public void SetProgressImmediately(int currentEnergy, int maxEnergy)
        {
            _tween?.Kill();
            
            slider.value = currentEnergy / (float)maxEnergy;
            progressView.SetProgressImmediately(currentEnergy, maxEnergy);
        }

        public void SetTimeLeft(float time)
        {
            timeView.SetTime(Mathf.CeilToInt(time));
        }
    }
}