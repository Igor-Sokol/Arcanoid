using Application.Scripts.Application.Scenes.Shared.LevelManagement.LevelPacks;
using Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.ProgressManagers.PackProgress.PacksInfo.Implementations
{
    public class PackInfoMono : MonoBehaviour, IPackInfo
    {
        [SerializeField] private LevelPack levelPack;
        [SerializeField] private int levelIndex;

        public LevelPack LevelPack => levelPack;
        public int CurrentLevelIndex => levelIndex;
    }
}