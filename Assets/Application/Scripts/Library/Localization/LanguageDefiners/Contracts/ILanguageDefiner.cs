using Application.Scripts.Library.Localization.Configs;
using UnityEngine;

namespace Application.Scripts.Library.Localization.LanguageDefiners.Contracts
{
    public interface ILanguageDefiner
    {
        LanguageConfig GetLanguage(LanguageConfig[] languageConfigs, SystemLanguage language);
    }
}