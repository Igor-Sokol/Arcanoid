using System;
using System.Collections.Generic;
using Application.Scripts.Library.Localization.Configs;
using UnityEngine;

namespace Application.Scripts.Library.Localization.LocalizationManagers
{
    public class LocalizationManager : MonoBehaviour
    {
        private LanguageConfig _currentLanguage;
        private Dictionary<string, string> _languageTable;

        [SerializeField] private LocalizationManagerConfig config;
        
        public event Action OnLanguageChanged;

        public string GetString(string key)
        {
            _languageTable.TryGetValue(key, out string value);
            return value;
        }
        
        public void LoadLanguage(SystemLanguage language = SystemLanguage.Unknown)
        {
            var languageConfig = config.LanguageDefiner.GetLanguage(config.LanguageSettings, language);

            if (!languageConfig)
            {
                languageConfig = config.DefaultLanguage;
            }
            _currentLanguage = languageConfig;

            _languageTable = new Dictionary<string, string>();
            
            var keyValuePairs = _currentLanguage.LanguageTableReader.ReadTable(_currentLanguage.LanguageInfo);
            foreach (var pair in keyValuePairs)
            {
                _languageTable.Add(pair.Key, pair.Value);
            }

            OnLanguageChanged?.Invoke();
        }
    }
}
