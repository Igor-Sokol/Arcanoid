using Application.Scripts.Application.Scenes.Shared.Energy.Contracts;
using Application.Scripts.Application.Scenes.Shared.UI.StringViews;
using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.InitializeManager.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Application.Scripts.Application.Scenes.ChoosePack.UI.EnergyView
{
    public class EnergyView : MonoBehaviour, IInitializing
    {
        private IEnergyManager _energyManager;
        
        [SerializeField] private Slider slider;
        [SerializeField] private ProgressView progressView;
        [SerializeField] private TimeView timeView;

        public void SetProgress(int currentEnergy, int maxEnergy)
        {
            slider.value = currentEnergy / (float)maxEnergy;
            progressView.SetProgress(currentEnergy, maxEnergy);
        }

        public void Initialize()
        {
            _energyManager = ProjectContext.Instance.GetService<IEnergyManager>();
            OnUpdateEnergy();

            _energyManager.OnEnergyAdded += OnUpdateEnergy;
            _energyManager.OnEnergyRemoved += OnUpdateEnergy;
            _energyManager.OnFillTimeChanged += OnTimeUpdate;
        }

        private void OnUpdateEnergy()
        {
            SetProgress(_energyManager.CurrentEnergy, _energyManager.MaxEnergy);
        }

        private void OnTimeUpdate(float time)
        {
            timeView.SetTime(Mathf.CeilToInt(time));
        }
    }
}