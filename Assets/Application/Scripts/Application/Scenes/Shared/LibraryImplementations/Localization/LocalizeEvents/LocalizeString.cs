using Application.Scripts.Library.Localization.LocalizationManagers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.Localization.LocalizeEvents
{
    public class LocalizeString : MonoBehaviour
    {
        private ILocalizationManager _localizationManager;
        
        [SerializeField] private TMP_Text updateString;
        [SerializeField] private string stringKey;

        [Inject]
        public void Construct(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
            SetLocalizationManager(_localizationManager);
        }
        
        private void OnEnable()
        {
            if (_localizationManager == null)
            {
                SetLocalizationManager(_localizationManager);
            }
            
            if (_localizationManager != null)
            {
                _localizationManager.OnLanguageChanged += OnUpdateString;
            }
        }

        private void OnDisable()
        {
            if (_localizationManager != null)
            {
                _localizationManager.OnLanguageChanged -= OnUpdateString;
            }
        }

        public void SetLocalizationManager(ILocalizationManager localization)
        {
            if (_localizationManager != null)
            {
                _localizationManager.OnLanguageChanged -= OnUpdateString;
            }
            
            _localizationManager = localization;
            OnUpdateString();
            _localizationManager.OnLanguageChanged += OnUpdateString;
        }

        private void OnUpdateString()
        {
            updateString.text = _localizationManager.GetString(stringKey);
        }
    }
}