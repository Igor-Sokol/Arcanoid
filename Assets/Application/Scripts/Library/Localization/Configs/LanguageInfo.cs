using System;
using UnityEngine;

namespace Application.Scripts.Library.Localization.Configs
{
    [Serializable]
    public struct LanguageInfo
    {
        [SerializeField] private SystemLanguage language;
        [SerializeField] private string path;

        public SystemLanguage Language => language;
        public string Path => path;

        public LanguageInfo(SystemLanguage language, string path)
        {
            this.language = language;
            this.path = path;
        }
    }
}