using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Levels.Services.Readers.Contracts
{
    public abstract class LevelReader : MonoBehaviour, ILevelReader
    {
        public abstract string[][] ReadPack(LevelInfo level);
    }
}