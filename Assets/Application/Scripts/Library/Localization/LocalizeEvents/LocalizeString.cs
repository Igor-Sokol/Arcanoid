using Application.Scripts.Library.Localization.LocalizationManagers;
using UnityEngine;
using UnityEngine.Events;

namespace Application.Scripts.Library.Localization.LocalizeEvents
{
    public class LocalizeString : MonoBehaviour
    {
        private LocalizationManager _localizationManager;
        
        [SerializeField] private string stringKey;
        [SerializeField] private UnityEvent<string> updateString;

        private void OnEnable()
        {
            if (_localizationManager)
            {
                _localizationManager.OnLanguageChanged += OnUpdateString;
            }
        }

        private void OnDisable()
        {
            if (_localizationManager)
            {
                _localizationManager.OnLanguageChanged -= OnUpdateString;
            }
        }

        public void SetLocalizationManager(LocalizationManager localization)
        {
            if (_localizationManager)
            {
                _localizationManager.OnLanguageChanged -= OnUpdateString;
            }
            
            _localizationManager = localization;
            OnUpdateString();
            _localizationManager.OnLanguageChanged += OnUpdateString;
        }

        private void OnUpdateString()
        {
            updateString?.Invoke(_localizationManager.GetString(stringKey));
        }
    }
}