using System.Collections.Generic;
using Application.Scripts.Library.Localization.Configs;
using UnityEngine;

namespace Application.Scripts.Library.Localization.LanguageTableReaders.Contracts
{
    public abstract class LanguageTableReader : MonoBehaviour, ILanguageTableReader
    {
        public abstract Dictionary<string, string> ReadTable(LanguageInfo languageInfo);
    }
}