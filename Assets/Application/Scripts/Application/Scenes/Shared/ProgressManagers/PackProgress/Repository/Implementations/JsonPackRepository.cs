using System.IO;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.ProgressObjects;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Repository.Contracts;
using Newtonsoft.Json;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.Repository.Implementations
{
    public class JsonPackRepository : PackProgressRepository
    {
        [SerializeField] private string filePath;
        
        public override void Save(PackProgressTransfer packProgressTransfer)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            string path = Path.Combine(UnityEngine.Application.persistentDataPath, filePath);
#else
            string path = Path.Combine(UnityEngine.Application.dataPath, filePath);
#endif

            var jsonFile = JsonConvert.SerializeObject(packProgressTransfer);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            File.WriteAllText(path, jsonFile);
        }

        public override PackProgressTransfer Load()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            string path = Path.Combine(UnityEngine.Application.persistentDataPath, filePath);
#else
            string path = Path.Combine(UnityEngine.Application.dataPath, filePath);
#endif

            if (File.Exists(path))
            {
                var jsonFile = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<PackProgressTransfer>(jsonFile);
            }

            return default;
        }
    }
}