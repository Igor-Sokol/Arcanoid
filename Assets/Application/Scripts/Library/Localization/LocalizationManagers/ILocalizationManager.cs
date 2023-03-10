using System;
using Application.Scripts.Library.Localization.Configs;
using UnityEngine;

namespace Application.Scripts.Library.Localization.LocalizationManagers
{
    public interface ILocalizationManager
    {
        LanguageConfig[] SupportedLanguages { get; }
        LanguageConfig CurrentLanguage { get; }
        event Action OnLanguageChanged;
        string GetString(string key);
        void LoadLanguage(SystemLanguage language);
    }
}