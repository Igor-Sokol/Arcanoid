using System;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Editor.LevelFormat
{
    public class LevelFormatCorrector : MonoBehaviour
    {
        [MenuItem("Assets/Fix Level Format")]
        private static void CorrectLevelFormat()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);

            try
            {
                var transferObject = JsonConvert.DeserializeObject<LevelTransferObject>(File.ReadAllText(path));
                string newFormat = JsonConvert.SerializeObject(transferObject.LevelInfos[0]);
                File.WriteAllText(path, newFormat);
                AssetDatabase.Refresh();
            }
            catch (Exception)
            {
                Debug.LogWarning("The file does not match the Tiled format.");
            }
        }
        
        [MenuItem("Assets/Fix Level Format", true)]
        private static bool CorrectLevelFormatValidation()
        {
            if (!Selection.activeObject) return false;
            
            return Selection.activeObject.GetType() == typeof(TextAsset);
        }
        
        private struct LevelTransferObject
        {
            [JsonProperty("layers")]
            public LevelInfo[] LevelInfos;
        }

        private struct LevelInfo
        {
            public int[] Data;
            public int Height;
            public int Width;
        }
    }
}