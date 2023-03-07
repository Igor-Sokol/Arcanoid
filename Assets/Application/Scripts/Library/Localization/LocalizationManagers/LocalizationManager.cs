using System;
using System.Collections.Generic;
using Application.Scripts.Library.Localization.Configs;
using Newtonsoft.Json;
using UnityEngine;

namespace Application.Scripts.Library.Localization.LocalizationManagers
{
    public class LocalizationManager : MonoBehaviour, ILocalizationManager
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
            
            _languageTable = _currentLanguage.LanguageTableReader.ReadTable(_currentLanguage.LanguageInfo);

            OnLanguageChanged?.Invoke();
        }
    }
}
