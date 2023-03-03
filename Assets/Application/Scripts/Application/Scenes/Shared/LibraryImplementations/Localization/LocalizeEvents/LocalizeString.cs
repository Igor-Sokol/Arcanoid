using Application.Scripts.Library.DependencyInjection;
using Application.Scripts.Library.Localization.LocalizationManagers;
using TMPro;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.Localization.LocalizeEvents
{
    public class LocalizeString : MonoBehaviour
    {
        private LocalizationManager _localizationManager;
        
        [SerializeField] private TMP_Text updateString;
        [SerializeField] private string stringKey;

        private void OnEnable()
        {
            if (!_localizationManager)
            {
                SetLocalizationManager(ProjectContext.Instance.GetService<LocalizationManager>());
            }
            
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
            updateString.text = _localizationManager.GetString(stringKey);
        }
    }
}