using Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Application.Scripts.Application.Scenes.Shared.LevelManagement.LevelPacks
{
    [CreateAssetMenu(fileName = "LevelPack", menuName = "LevelManagement/LevelPack")]
    public class LevelPack : ScriptableObject
    {
        [SerializeField] private string packNameKey;
        [SerializeField] private Sprite packImage;
        [SerializeField] private ScriptableLevelInfo[] levels;

        public string PackNameKey => packNameKey;
        public Sprite PackImage => packImage;
        public ScriptableLevelInfo[] Levels => levels;
    }
}