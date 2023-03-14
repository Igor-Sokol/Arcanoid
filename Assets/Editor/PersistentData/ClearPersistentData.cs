using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.PersistentData
{
    public class ClearPersistentData : MonoBehaviour
    {
        [MenuItem("Edit/Clear All PersistentData", priority = 275)]
        private static void Clear()
        {
            if (EditorUtility.DisplayDialog("Clear All PersistentData", 
                    "Are you sure want to clear all PersistentData? This action cannot be undone.",
                    "Yes", "No"))
            {
                DirectoryInfo dataDir = new DirectoryInfo(UnityEngine.Application.persistentDataPath);
                dataDir.Delete(true);
            }
        }
    }
}