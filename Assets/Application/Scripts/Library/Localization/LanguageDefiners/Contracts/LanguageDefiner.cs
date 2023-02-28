using Application.Scripts.Library.Localization.Configs;
using UnityEngine;

namespace Application.Scripts.Library.Localization.LanguageDefiners.Contracts
{
    public abstract class LanguageDefiner : MonoBehaviour, ILanguageDefiner
    {
        public abstract LanguageConfig GetLanguage(LanguageConfig[] languageConfigs, SystemLanguage language);
    }
}