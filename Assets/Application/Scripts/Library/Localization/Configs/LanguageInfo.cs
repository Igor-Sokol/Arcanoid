using System;
using UnityEngine;

namespace Application.Scripts.Library.Localization.Configs
{
    [Serializable]
    public struct LanguageInfo
    {
        [SerializeField] private SystemLanguage language;
        [SerializeField] private Sprite flag;
        [SerializeField] private string languageKey;
        [SerializeField] private string path;

        public SystemLanguage Language => language;
        public Sprite Flag => flag;
        public string LanguageKey => languageKey;
        public string Path => path;

        public LanguageInfo(SystemLanguage language, Sprite flag, string languageKey, string path)
        {
            this.language = language;
            this.flag = flag;
            this.languageKey = languageKey;
            this.path = path;
        }
    }
}