using Application.Scripts.Application.Scenes.Game.Pools.BlockProviders.Contracts;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Shared.LevelManagement.Levels.Readers.Contracts
{
    public abstract class LevelReader : MonoBehaviour, ILevelReader
    {
        public abstract BlockInfo[][] ReadPack(LevelInfo level);
    }
}