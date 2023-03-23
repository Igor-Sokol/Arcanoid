using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.Contracts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LevelManagement.LevelPacks
{
    [CreateAssetMenu(fileName = "LevelPack", menuName = "LevelManagement/LevelPack")]
    public class LevelPack : ScriptableObject
    {
        [SerializeField] private string packNameKey;
        [SerializeField] private Sprite packImage;
        [SerializeField] private LevelReader levelReader;
        [SerializeField] private string packPath;
        [SerializeField] private List<string> levelNames;

        public string PackNameKey => packNameKey;
        public Sprite PackImage => packImage;
        public LevelInfo[] Levels => ReadLevels();
        public int LevelCount => levelNames.Count;

        [Conditional("UNITY_EDITOR"), Button("Load Levels")]
        private void LoadLevels()
        {
            var assets = Resources.LoadAll<TextAsset>(packPath);
            foreach (var asset in assets)
            {
                if (!levelNames.Contains(asset.name))
                {
                    levelNames.Add(asset.name);
                }
                Resources.UnloadAsset(asset);
            }
        }
        
        private LevelInfo[] ReadLevels()
        {
            return levelNames.Select(p => new LevelInfo(packPath + p, levelReader)).ToArray();
        }
    }
}