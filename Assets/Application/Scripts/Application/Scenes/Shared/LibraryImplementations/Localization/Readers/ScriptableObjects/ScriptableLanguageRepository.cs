using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LibraryImplementations.Localization.Readers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ScriptableLanguage", menuName = "Localization/Repositories/ScriptableLanguages")]
    public class ScriptableLanguageRepository : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<string, string> table;

        public Dictionary<string, string> Table => table;
    }
}