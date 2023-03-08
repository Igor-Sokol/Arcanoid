using System.IO;
using Application.Scripts.Application.Scenes.Shared.Energy.Repository.Contracts;
using Newtonsoft.Json;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.Energy.Repository.Implementations
{
    public class JsonEnergyRepository : EnergyRepository
    {
        [SerializeField] private string filePath;
        
        public override void Save(EnergySaveObject energySave)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            string path = Path.Combine(UnityEngine.Application.persistentDataPath, filePath);
#else
            string path = Path.Combine(UnityEngine.Application.dataPath, filePath);
#endif

            var jsonFile = JsonConvert.SerializeObject(energySave);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            File.WriteAllText(path, jsonFile);
        }

        public override EnergySaveObject Load()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            string path = Path.Combine(UnityEngine.Application.persistentDataPath, filePath);
#else
            string path = Path.Combine(UnityEngine.Application.dataPath, filePath);
#endif

            if (File.Exists(path))
            {
                var jsonFile = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<EnergySaveObject>(jsonFile);
            }

            return default;
        }
    }
}