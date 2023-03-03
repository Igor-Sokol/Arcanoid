using System.Linq;
using Application.Scripts.Library.Localization.Configs;
using Application.Scripts.Library.Localization.LanguageDefiners.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.Localization.Definers
{
    public class StandardLanguageDefiner : LanguageDefiner
    {
        public override LanguageConfig GetLanguage(LanguageConfig[] languageConfigs, SystemLanguage language)
        {
            LanguageConfig languageConfig = null;

            if (language == SystemLanguage.Unknown)
            {
                languageConfig = languageConfigs
                    .FirstOrDefault(l => l.LanguageInfo.Language == UnityEngine.Application.systemLanguage);
            }
            else
            {
                languageConfig = languageConfigs.FirstOrDefault(l => l.LanguageInfo.Language == language);
            }

            return languageConfig;
        }
    }
}