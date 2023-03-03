using System.Collections.Generic;
using Application.Scripts.Library.Localization.Configs;
using Application.Scripts.Library.Localization.LanguageTableReaders.Contracts;
using Newtonsoft.Json;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.Localization.Readers.Json
{
    public class JsonLanguageTableReader : LanguageTableReader
    {
        public override Dictionary<string, string> ReadTable(LanguageInfo languageInfo)
        {
            TextAsset languageFilePath = Resources.Load<TextAsset>(languageInfo.Path);

            var table = JsonConvert.DeserializeObject<Dictionary<string, string>>(languageFilePath.text);
            Resources.UnloadAsset(languageFilePath);

            return table;
        }
    }
}