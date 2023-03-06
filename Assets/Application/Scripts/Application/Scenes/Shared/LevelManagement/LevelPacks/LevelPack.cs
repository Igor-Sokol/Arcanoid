using System.Linq;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels;
using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.Contracts;
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
        [SerializeField] private string[] levelNames;

        public string PackNameKey => packNameKey;
        public Sprite PackImage => packImage;
        public LevelInfo[] Levels => ReadLevels();
        public int LevelCount => levelNames.Length;

        private LevelInfo[] ReadLevels()
        {
            return levelNames.Select(p => new LevelInfo(packPath + p, levelReader)).ToArray();
        }
    }
}