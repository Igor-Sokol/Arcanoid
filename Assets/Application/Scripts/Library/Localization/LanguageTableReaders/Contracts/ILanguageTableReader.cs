using System.Collections.Generic;
using Application.Scripts.Library.Localization.Configs;

namespace Application.Scripts.Library.Localization.LanguageTableReaders.Contracts
{
    public interface ILanguageTableReader
    {
        Dictionary<string, string> ReadTable(LanguageInfo languageInfo);
    }
}