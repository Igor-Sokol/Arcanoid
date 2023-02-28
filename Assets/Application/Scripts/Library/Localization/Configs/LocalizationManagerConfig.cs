using Application.Scripts.Library.Localization.LanguageDefiners.Contracts;
using UnityEngine;

namespace Application.Scripts.Library.Localization.Configs
{
    [CreateAssetMenu(fileName = "LocalizationManager", menuName = "Localization/LocalizationManager")]
    public class LocalizationManagerConfig : ScriptableObject
    {
        [SerializeField] private LanguageConfig[] languageSettings;
        [SerializeField] private LanguageConfig defaultLanguage;
        [SerializeField] private LanguageDefiner languageDefiner;

        public LanguageConfig[] LanguageSettings => languageSettings;
        public LanguageConfig DefaultLanguage => defaultLanguage;
        public LanguageDefiner LanguageDefiner => languageDefiner;
    }
}