using Application.Scripts.Application.Scenes.Shared.UI.StringViews;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.Shared.UI.EnergyViews
{
    public class EnergyView : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private ProgressView progressView;
        [SerializeField] private TimeView timeView;
        [SerializeField] private float duration;

        public void SetProgress(int currentEnergy, int maxEnergy)
        {
            slider.DOValue(currentEnergy / (float)maxEnergy, duration);
            progressView.SetProgress(currentEnergy, maxEnergy);
        }

        public void SetTimeLeft(float time)
        {
            timeView.SetTime(Mathf.CeilToInt(time));
        }
    }
}