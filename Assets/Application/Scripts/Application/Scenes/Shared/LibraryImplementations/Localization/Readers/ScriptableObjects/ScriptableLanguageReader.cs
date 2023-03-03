using System.Collections.Generic;
using Application.Scripts.Library.Localization.Configs;
using Application.Scripts.Library.Localization.LanguageTableReaders.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.Localization.Readers.ScriptableObjects
{
    public class ScriptableLanguageReader : LanguageTableReader
    {
        public override Dictionary<string, string> ReadTable(LanguageInfo languageInfo)
        {
            var language = Resources.Load<ScriptableLanguageRepository>(languageInfo.Path);

            var table = new Dictionary<string, string>(language.Table);
            Resources.UnloadAsset(language);

            return table;
        }
    }
}