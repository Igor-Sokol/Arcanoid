using Application.Scripts.Library.Localization.LanguageTableReaders.Contracts;
using UnityEngine;

namespace Application.Scripts.Library.Localization.Configs
{
    [CreateAssetMenu(fileName = "Language", menuName = "Localization/Language")]
    public class LanguageConfig : ScriptableObject
    {
        [SerializeField] private LanguageInfo languageInfo;
        [SerializeField] private LanguageTableReader languageTableReader;

        public LanguageInfo LanguageInfo => languageInfo;
        public LanguageTableReader LanguageTableReader => languageTableReader;
    }
}