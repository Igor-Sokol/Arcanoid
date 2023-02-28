using System.Collections.Generic;
using System.IO;
using Application.Scripts.Library.Localization.Configs;
using Application.Scripts.Library.Localization.LanguageTableReaders.Contracts;
using Newtonsoft.Json;

namespace Application.Scripts.Library.Localization.LanguageTableReaders.Implementations
{
    public class JsonLanguageTableReader : LanguageTableReader
    {
        public override IEnumerable<KeyValuePair<string, string>> ReadTable(LanguageInfo languageInfo)
        {
            string languageFilePath;
            
#if UNITY_EDITOR
            languageFilePath = Path.Combine(UnityEngine.Application.dataPath, languageInfo.Path);
#else
            languageFilePath = Path.Combine(UnityEngine.Application.persistentDataPath, languageInfo.Path);
#endif
            
            string text = File.ReadAllText(languageFilePath);
            return JsonConvert.DeserializeObject<KeyValuePair<string, string>[]>(text);
        }
    }
}