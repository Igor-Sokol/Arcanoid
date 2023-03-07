using System;
using UnityEngine;

namespace Application.Scripts.Library.Localization.LocalizationManagers
{
    public interface ILocalizationManager
    {
        event Action OnLanguageChanged;
        string GetString(string key);
        void LoadLanguage(SystemLanguage language);
    }
}