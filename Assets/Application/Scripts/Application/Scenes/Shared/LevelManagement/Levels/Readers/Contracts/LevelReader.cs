using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.Contracts
{
    public abstract class LevelReader : MonoBehaviour, ILevelReader
    {
        public abstract string[][] ReadPack(LevelInfo level);
    }
}