using Application.Scripts.Application.Scenes.Shared.LevelManagement.LevelPacks;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LevelManagement
{
    [CreateAssetMenu(fileName = "PackManager", menuName = "LevelManagement/PackManager")]
    public class PackManager : ScriptableObject
    {
        [SerializeField] private LevelPack[] levelPacks;

        public LevelPack[] LevelPacks => levelPacks;
    }
}