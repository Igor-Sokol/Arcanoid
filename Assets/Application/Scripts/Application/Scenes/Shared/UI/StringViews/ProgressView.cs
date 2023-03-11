using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.UI.StringViews
{
    public class ProgressView : MonoBehaviour
    {
        private Tween _tween;
        private int _currentValue;
        private int _maxValue;
        
        [SerializeField] private TMP_Text progressText;
        [SerializeField] private string mask;
        [SerializeField] private float progressDuration;

        public Tween SetProgress(int current, int max)
        {
            _maxValue = max;

            _tween?.Kill();
            _tween = DOTween.To(() => _currentValue, SetCurrentProgress, current, progressDuration);

            return _tween;
        }
        
        public void SetProgressImmediately(int current, int max)
        {
            _maxValue = max;

            _tween?.Kill();
            SetCurrentProgress(current);
        }

        private void SetCurrentProgress(int progress)
        {
            _currentValue = progress;
            progressText.text = string.Format(mask, _currentValue, _maxValue);
        }
    }
}